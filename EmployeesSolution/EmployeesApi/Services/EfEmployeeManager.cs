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

    public async Task<EmployeeSummaryListResponse> GetAllEmployeesAsync(string department)
    {

        var employees = GetEmployees();
        if(department != "All")
        {
            employees = employees.Where(e => e.Department == department);
        }
        var result = await employees
            // Given I have an EmployeeEntity -> EmployeeSummaryListItemResponse
            .Select(emp => new EmployeeSummaryListItemResponse
            {
                Id = emp.Id.ToString(),
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Department = emp.Department,
                EmailAddress = emp.EmailAddress,
            })
            .ToListAsync(); // "Non-Deferred Operator"


        var response = new EmployeeSummaryListResponse
        {
            Employees = result,
            ShowingDepartment = department
        };
        return response;
    }

    public async Task<EmployeeDetailsItemResponse?> GetEmployeeByIdAsync(string id)
    {
        if (int.TryParse(id, out var convertedId)) {
            return await GetEmployees()
                .Where(e => e.Id == convertedId)
                .Select(emp => new EmployeeDetailsItemResponse
                {
                    Id = emp.Id.ToString(),
                    Department = emp.Department,
                    EmailAddress = emp.EmailAddress,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    PhoneNumber = emp.PhoneNumber,
                }).SingleOrDefaultAsync();

            } return null;
    }

    private IQueryable<EmployeeEntity> GetEmployees()
    {
        return _dataContext.Employees;
    }
}
