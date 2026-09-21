using System.ComponentModel.DataAnnotations;

namespace Saksmappe.Api.DTOs;

public class UpdateApplicantDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nationality { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }
}