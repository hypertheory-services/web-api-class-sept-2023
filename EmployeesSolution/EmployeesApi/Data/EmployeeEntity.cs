

namespace EmployeesApi.Data;

public class EmployeeEntity
{
    public int Id { get; set; }
  
    public string FirstName { get; set; } = string.Empty;
  
    public string LastName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
  
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}
