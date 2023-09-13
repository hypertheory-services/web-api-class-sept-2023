using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.Models;

public record DepartmentHiringRequest
{
    [Required]
    public string CandidateId { get; set; } = string.Empty;
    [Required]
    public decimal? StartingSalary { get; set; }
}

public record DepartmentHiringRequestResponse
{
    public int Id { get; set; }
    public string CandidateId { get; set; } = string.Empty;
    public decimal? StartingSalary { get; set; }
    public HiringRequestStatus Status { get; set; } = HiringRequestStatus.Hired;
}
public enum HiringRequestStatus {  Hired }

public record EmployeeContactMechanismsResponse
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}