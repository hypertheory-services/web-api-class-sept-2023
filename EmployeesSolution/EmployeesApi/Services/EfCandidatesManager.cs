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

    private readonly List<string> _departments = new()
    {
        "dev",
        "qa",
        "hr"
    };

    public EfCandidatesManager(EmployeesDataContext context, IMapper mapper, MapperConfiguration mapperConfig, IProvideTheTelecomApi telecomApi)
    {
        _context = context;
        _mapper = mapper;
        _mapperConfig = mapperConfig;
        _telecomApi = telecomApi;
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
        if(int.TryParse(id, out var candidateId))
        {
            return await _context.Candidates.Where(c => c.Id == candidateId)
                .ProjectTo<CandidateResponseModel>(_mapperConfig)
                .SingleOrDefaultAsync();
        }
        return null;
    }

    public async Task<CandidateHiringResponse> HireCandidateAsync(string department,DepartmentHiringRequest request)
    {

        if (int.TryParse(request.CandidateId, out var candidateId))
        {
            // 2. Validate that
            //    - Does the Department exist?
            var departmentExists = _departments.Any(d => d == department.ToLowerInvariant());
            if (!departmentExists)
            { 
                return new CandidateHiringResponse { DepartmentNotFound = true };
            }// the department isn't found.
            //    - Is the salary >= minSalary
            var candidate = await _context.Candidates
                .Where(c => c.Id == candidateId && c.Status == CandidateStatus.AwaitingManager)
              
                .SingleOrDefaultAsync();

            if(candidate is null)
            {
                return new CandidateHiringResponse { CandidateNotAvailable = true }; // this time it means there is no candidate that matches the criteria (has the id, and is in the right "state")
            }

            if(candidate.RequiredSalaryMin < request.StartingSalary)
            {
                return new CandidateHiringResponse {  IncorrectSalaryOffered = true };
            }


            // 3. We need to an email address and a phone number for them?
            EmployeeContactMechanismsResponse phoneAndEmailAssignment = await _telecomApi.GetPhoneAndEmailAssignmentAsync(candidate.FirstName, candidate.LastName);
            // 4. We need to update their candidate resource to change their status AND
            candidate.Status = CandidateStatus.Hired;
            //    We need to add them as an employee.
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
            // 5. Send a response ??
            //
            return new CandidateHiringResponse
            {
                Candidate = new CandidateResponseModel
                {
                    // TODO: Add the employee stuff and all that here. Maybe Automapper.
                }
            };

        }
        return null; // kind of overusing null here. there is no candidate with that id?
    }
}

public record CandidateHiringResponse
{
    public bool DepartmentNotFound { get; set; } = false;
    public bool CandidateNotAvailable { get; set; } = false;
    public bool IncorrectSalaryOffered { get; set; } = false;
    public CandidateResponseModel? Candidate { get; set; }

}
