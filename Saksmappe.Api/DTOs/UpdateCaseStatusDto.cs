using System.ComponentModel.DataAnnotations;
using Saksmappe.Api.Enums;

namespace Saksmappe.Api.DTOs;

public class UpdateCaseStatusDto
{
    [Required]
    public CaseStatus Status { get; set; }
}