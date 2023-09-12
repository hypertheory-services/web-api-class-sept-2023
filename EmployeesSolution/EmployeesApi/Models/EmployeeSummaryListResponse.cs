using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.Models;

public record EmployeeSummaryListResponse
{
    [Required]
    public List<EmployeeSummaryListItemResponse> Employees { get; set; } = new();

    public string? ShowingDepartment { get; set; }
}

