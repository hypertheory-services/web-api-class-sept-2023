using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.Models;


public class EmployeeDetailsItemResponse
{
    [Required]
    public string Id { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
        
    public string Department { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;
}

