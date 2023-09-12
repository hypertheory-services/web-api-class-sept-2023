namespace EmployeesApi.Services;

public interface IManageCandidates
{
    Task<CandidateResponseModel> CreateCandidateAsync(CandidateRequestModel request);
    Task<CandidateResponseModel?> GetCandidateByIdAsync(string id);
}
