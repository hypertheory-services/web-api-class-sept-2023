namespace EmployeesApi.Services;

public interface IManageEmployees
{
    Task<EmployeeSummaryListResponse> GetAllEmployeesAsync();
}
