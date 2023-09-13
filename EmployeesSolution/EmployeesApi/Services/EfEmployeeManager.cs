using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesApi.Controllers;
using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Services;

public class EfEmployeeManager : IManageEmployees
{
    private readonly EmployeesDataContext _dataContext;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _mapperConfig;

    public EfEmployeeManager(EmployeesDataContext dataContext, IMapper mapper, MapperConfiguration mapperConfig)
    {
        _dataContext = dataContext;
        _mapper = mapper;
        _mapperConfig = mapperConfig;
    }

    public async Task<EmployeeSummaryListResponse> GetAllEmployeesAsync(string department)
    {

        var employees = GetEmployees();
        if (department != "All")
        {
            employees = employees.Where(e => e.Department == department);
        }
        var result = await employees
            // Given I have an EmployeeEntity -> EmployeeSummaryListItemResponse
            .ProjectTo<EmployeeSummaryListItemResponse>(_mapperConfig)
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
        if (int.TryParse(id, out var convertedId))
        {
            return await GetEmployees()
                .Where(e => e.Id == convertedId)
                .ProjectTo<EmployeeDetailsItemResponse>(_mapperConfig)
                .SingleOrDefaultAsync();

        }
        return null;
    }

    public async Task<EmployeeSalaryResponseModel?> GetSalaryForEmployeeAsync(string id)
    {
        var emp = await GetEmployees()
            .Where(e => e.Id == int.Parse(id)) // Bad. Just in a hurry.
             .SingleOrDefaultAsync();
        if(emp is null )
        {
            return null;
        }
        var response = new EmployeeSalaryResponseModel
        {
            Type = "annual",
            Amount = emp.Salary
        };
        return response;
    }

    private IQueryable<EmployeeEntity> GetEmployees()
    {
        return _dataContext.Employees;
    }
}
