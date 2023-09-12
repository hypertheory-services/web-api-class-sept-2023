using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Services;

public class EfEmployeeManager : IManageEmployees
{
    private readonly EmployeesDataContext _dataContext;

    public EfEmployeeManager(EmployeesDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<EmployeeSummaryListResponse> GetAllEmployeesAsync()
    {
        var employees = await _dataContext.Employees
            .Select(emp => new EmployeeSummaryListItemResponse
            {
                Id = emp.Id.ToString(),
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Department = emp.Department,
                EmailAddress = emp.EmailAddress,
            })
            .ToListAsync();

        var response = new EmployeeSummaryListResponse
        {
            Employees = employees
        };
        return response;
    }
}
