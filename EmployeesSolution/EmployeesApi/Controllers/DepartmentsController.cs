namespace EmployeesApi.Controllers;

[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IManageCandidates _candidateManager;

    public DepartmentsController(IManageCandidates candidateManager)
    {
        _candidateManager = candidateManager;
    }

    [HttpPost("/departments/{department}/hiring-requests")]
    public async Task<ActionResult<CandidateResponseModel>> CreateHiringRequest([FromBody] DepartmentHiringRequest request, string department)
    {
        CandidateResponseModel? response = await _candidateManager.HireCandidateAsync(department, request);
        return Ok();
    }
    //[HttpPost("/departments/dev/hiring-requests")]
    //public async Task<ActionResult<CandidateResponseModel>> CreateHiringRequest([FromBody] DepartmentHiringRequest request)
    //{
    //    CandidateResponseModel? response = await _candidateManager.HireCandidateAsync("dev", request);
    //    return Ok();
    //}
}
