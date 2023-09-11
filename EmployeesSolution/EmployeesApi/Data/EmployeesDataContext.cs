using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Data;

public class EmployeesDataContext : DbContext
{
    public EmployeesDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<EmployeeEntity> Employees { get; set; }
}
