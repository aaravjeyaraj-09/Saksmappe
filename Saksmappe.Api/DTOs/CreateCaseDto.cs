using System.ComponentModel.DataAnnotations;
using Saksmappe.Api.Enums;

namespace Saksmappe.Api.DTOs;

public class CreateCaseDto
{
    [Required]
    public Guid ApplicantId { get; set; }

    [Required]
    public CaseType CaseType { get; set; }
}