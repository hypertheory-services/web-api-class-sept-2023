

using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Controllers;

public class EmployeesController : ControllerBase
{
    private readonly IManageEmployees _employeeManager;

    public EmployeesController(IManageEmployees employeeManager)
    {
        _employeeManager = employeeManager;
    }

    [HttpGet("/employees")]
    public async Task<ActionResult<EmployeeSummaryListResponse>> GetAllEmployees([FromQuery] string department = "All")
    {
        EmployeeSummaryListResponse response = await _employeeManager.GetAllEmployeesAsync(department);
        return Ok(response);
    }

    [HttpGet("/employees/{id}")]
    public async Task<ActionResult<EmployeeDetailsItemResponse>> GetAnEmployee(string id)
    {
        EmployeeDetailsItemResponse? response = await _employeeManager.GetEmployeeByIdAsync(id);
       
        if(response is null)
        {
            return NotFound();
        } else
        {
            return Ok(response);
        }
    }
}
