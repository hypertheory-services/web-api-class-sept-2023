using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Services;

public class EfCandidatesManager : IManageCandidates
{
    private readonly IProvideTheTelecomApi _telecomApi;
    private readonly EmployeesDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _mapperConfig;
    private readonly ILogger<EfCandidatesManager> _logger;

    private readonly List<string> _departments = new()
    {
        "dev",
        "qa",
        "hr"
    };

    public EfCandidatesManager(EmployeesDataContext context, IMapper mapper, MapperConfiguration mapperConfig, IProvideTheTelecomApi telecomApi, ILogger<EfCandidatesManager> logger)
    {
        _context = context;
        _mapper = mapper;
        _mapperConfig = mapperConfig;
        _telecomApi = telecomApi;
        _logger = logger;
    }

    public async Task<CandidateResponseModel> CreateCandidateAsync(CandidateRequestModel request)
    {

        var candidate = _mapper.Map<CandidateEntity>(request);
        // candidate.Status = // whatever code you need to write!
        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();
        return _mapper.Map<CandidateResponseModel>(candidate);

    }

    public async Task<CandidateResponseModel?> GetCandidateByIdAsync(string id)
    {
        if (int.TryParse(id, out var candidateId))
        {
            return await _context.Candidates.Where(c => c.Id == candidateId)
                .ProjectTo<CandidateResponseModel>(_mapperConfig)
                .SingleOrDefaultAsync();
        }
        return null;
    }

    public async Task<CandidateHiringResponse> HireCandidateAsync(string department, DepartmentHiringRequest request)
    {
        int candidateId = -11;
        
        

        if (!int.TryParse(request.CandidateId, out candidateId))
        {
            return new CandidateHiringResponse.CandidateNotAvailable();
        }
        
        var departmentExists = _departments.Any(d => d == department.ToLowerInvariant());
        if (!departmentExists)
        {
            return new CandidateHiringResponse.DepartmentNotFound();
        }
        var candidate = await _context.Candidates
            .Where(c => c.Id == candidateId && c.Status == CandidateStatus.AwaitingManager)
            .SingleOrDefaultAsync();

        if (candidate is null)
        {
            return new CandidateHiringResponse.CandidateNotAvailable();
        }

        if (candidate.RequiredSalaryMin >= request.StartingSalary)
        {
            return new CandidateHiringResponse.IncorrectSalaryOffered();
        }

        EmployeeContactMechanismsResponse phoneAndEmailAssignment;
        try
        {
            phoneAndEmailAssignment = await _telecomApi.GetPhoneAndEmailAssignmentAsync(candidate.FirstName, candidate.LastName);
        }
        catch (Exception)
        {

            phoneAndEmailAssignment = new EmployeeContactMechanismsResponse
            {
                Email = "Unable to Assign - Check Back",
                PhoneNumber = "Unable to Assign - Check Back"
            };

            _logger.LogError("Unable to assign email and phone number to candidate {0}", candidate.Id);
        }
        
        candidate.Status = CandidateStatus.Hired;
        var newEmployee = new EmployeeEntity
        {
            Department = department,
            EmailAddress = phoneAndEmailAssignment.Email,
            PhoneNumber = phoneAndEmailAssignment.PhoneNumber,
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            Salary = request.StartingSalary!.Value
        };
        _context.Employees.Add(newEmployee);
        await _context.SaveChangesAsync();


        return new CandidateHiringResponse.CandidateHired(_mapper.Map<CandidateResponseModel>(candidate));
     
    }
}

public abstract record CandidateHiringResponse
{
    public record DepartmentNotFound : CandidateHiringResponse { }
    public record CandidateNotAvailable : CandidateHiringResponse { }
    public record IncorrectSalaryOffered : CandidateHiringResponse { }
    public record CandidateHired(CandidateResponseModel response) : CandidateHiringResponse { }
}
