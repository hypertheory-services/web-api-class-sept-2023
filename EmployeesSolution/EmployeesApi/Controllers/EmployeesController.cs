
namespace EmployeesApi.Controllers;

public class EmployeesController : ControllerBase
{
    [HttpGet("/employees")]
    public async Task<ActionResult<EmployeeSummaryListResponse>> GetAllEmployees()
    {
        return Ok();
    }

    [HttpGet("/employees/{id}")]
    public async Task<ActionResult<EmployeeDetailsItemResponse>> GetAnEmployee(string id)
    {
        return Ok();
    }
}
