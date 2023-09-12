namespace EmployeesApi.Services;

public interface IManageEmployees
{
    Task<EmployeeSummaryListResponse> GetAllEmployeesAsync(string department);
    Task<EmployeeDetailsItemResponse?> GetEmployeeByIdAsync(string id);
}
