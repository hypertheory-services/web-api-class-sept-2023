using EmployeesApi.Controllers;

namespace EmployeesApi.Services;

public interface IManageEmployees
{
    Task<EmployeeSummaryListResponse> GetAllEmployeesAsync(string department);
    Task<EmployeeDetailsItemResponse?> GetEmployeeByIdAsync(string id);
    Task<EmployeeSalaryResponseModel?> GetSalaryForEmployeeAsync(string id);
}
