namespace Saksmappe.Api.Models;

public class Applicant
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Nationality { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public ICollection<Case> Cases { get; set; } = new List<Case>();
}