using Saksmappe.Api.Enums;

namespace Saksmappe.Api.DTOs;

public class CaseResponseDto
{
    public Guid Id { get; set; }

    public string CaseNumber { get; set; } = string.Empty;

    public CaseType CaseType { get; set; }

    public CaseStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid ApplicantId { get; set; }
}