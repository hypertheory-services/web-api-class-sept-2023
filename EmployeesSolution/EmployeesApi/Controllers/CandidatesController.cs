namespace EmployeesApi.Controllers;

[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly IManageCandidates _candidateManager;

    public CandidatesController(IManageCandidates candidateManager)
    {
        _candidateManager = candidateManager;
    }

    [HttpPost("/candidates")]
    public async Task<ActionResult<CandidateResponseModel>> AddACandidate([FromBody] CandidateRequestModel request)
    {
        CandidateResponseModel response = await _candidateManager.CreateCandidateAsync(request);
        return CreatedAtRoute("candidates-getbyid", new { id=response.Id }, response);
    }

    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
    [HttpGet("/candidates/{id}", Name = "candidates-getbyid")]
    public async Task<ActionResult<CandidateResponseModel>> GetCandidateByid(string id)
    {
        CandidateResponseModel? response = await _candidateManager.GetCandidateByIdAsync(id);
        if(response is null)
        {
            return NotFound();
        }
        return Ok(response);
    }
}
