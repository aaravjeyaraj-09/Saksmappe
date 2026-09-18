namespace Saksmappe.Api.Models;

public class Case
{
    public Guid Id { get; set; }

    public string CaseNumber { get; set; } = string.Empty;

    public string CaseType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Applicant Applicant { get; set; } = null!;
}