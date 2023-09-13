using System.Reflection.Metadata.Ecma335;

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
        var response = await _candidateManager.HireCandidateAsync(department, request);


        return response switch
        {
            CandidateHiringResponse.CandidateNotAvailable => NotFound("Candidate not available"),
            CandidateHiringResponse.DepartmentNotFound => NotFound("Department not found"),
            CandidateHiringResponse.IncorrectSalaryOffered => BadRequest("Did not match Salary Requirement"),
            CandidateHiringResponse.CandidateHired c => Ok(c.response),
            _ => BadRequest() // unhandled case?
        }; 
    }
}

