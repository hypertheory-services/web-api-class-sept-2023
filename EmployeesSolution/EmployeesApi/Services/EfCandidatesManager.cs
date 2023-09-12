using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Services;

public class EfCandidatesManager : IManageCandidates
{

    private readonly EmployeesDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _mapperConfig;

    public EfCandidatesManager(EmployeesDataContext context, IMapper mapper, MapperConfiguration mapperConfig)
    {
        _context = context;
        _mapper = mapper;
        _mapperConfig = mapperConfig;
    }

    public async Task<CandidateResponseModel> CreateCandidateAsync(CandidateRequestModel request)
    {

        var candidate = _mapper.Map<CandidateEntity>(request);
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
}
