using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saksmappe.Api.Data;
using Saksmappe.Api.Models;
using Saksmappe.Api.DTOs;

namespace Saksmappe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicantsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApplicantsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicants()
{ 
    var applicants = await _context.Applicants.ToListAsync();

    return Ok(applicants);
}

 [HttpGet("{id}")]
public async Task<IActionResult> GetApplicant(Guid id)
{
    var applicant = await _context.Applicants.FindAsync(id);

    if (applicant == null)
    {
        return NotFound();
    }

    return Ok(applicant);
}

[HttpPost]
public async Task<IActionResult> CreateApplicant(CreateApplicantDto dto)
{
    var applicant = new Applicant
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Email = dto.Email,
        Nationality = dto.Nationality,
        DateOfBirth = dto.DateOfBirth,
        CreatedAt = DateTime.UtcNow
    };

    _context.Applicants.Add(applicant);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetApplicant), new { id = applicant.Id }, applicant);
}

[HttpPut("{id:Guid}")]
public async Task<IActionResult> UpdateApplicant(Guid id, UpdateApplicantDto dto)
{
    var applicant = await _context.Applicants.FindAsync(id);

    if (applicant == null)
    {
        return NotFound();
    }

    applicant.FirstName = dto.FirstName;
    applicant.LastName = dto.LastName;
    applicant.Email = dto.Email;
    applicant.Nationality = dto.Nationality;
    applicant.DateOfBirth = dto.DateOfBirth;

    await _context.SaveChangesAsync();

    return Ok(applicant);
}

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteApplicant(Guid id)
    {
        var applicant = await _context.Applicants.FindAsync(id);

        if (applicant == null)
        {
            return NotFound();
        }

        _context.Applicants.Remove(applicant);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

