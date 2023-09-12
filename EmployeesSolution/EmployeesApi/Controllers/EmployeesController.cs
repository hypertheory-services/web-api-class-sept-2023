

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
    public async Task<ActionResult<EmployeeSummaryListResponse>> GetAllEmployees()
    {

        // "Write the Code You Wish You Had"
        EmployeeSummaryListResponse response = await _employeeManager.GetAllEmployeesAsync();
        return Ok(response);
    }

    [HttpGet("/employees/{id}")]
    public async Task<ActionResult<EmployeeDetailsItemResponse>> GetAnEmployee(string id)
    {
        return Ok();
    }
}
