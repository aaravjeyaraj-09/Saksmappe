using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saksmappe.Api.Data;
using Saksmappe.Api.DTOs;
using Saksmappe.Api.Enums;
using Saksmappe.Api.Models;
using Saksmappe.Api.Services;

namespace Saksmappe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CaseWorkflowService _workflowService;

    public CasesController(
        AppDbContext context,
        CaseWorkflowService workflowService)
    {
        _context = context;
        _workflowService = workflowService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCase(CreateCaseDto dto)
    {
        var applicantExists = await _context.Applicants
            .AnyAsync(a => a.Id == dto.ApplicantId);

        if (!applicantExists)
        {
            return BadRequest(new
            {
                message = "Applicant does not exist."
            });
        }

        var now = DateTime.UtcNow;

        var caseItem = new Case
        {
            Id = Guid.NewGuid(),
            CaseNumber = $"SAK-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            CaseType = dto.CaseType,
            Status = CaseStatus.Received,
            CreatedAt = now,
            UpdatedAt = now,
            ApplicantId = dto.ApplicantId
        };

        _context.Cases.Add(caseItem);
        await _context.SaveChangesAsync();

        var response = new CaseResponseDto
        {
            Id = caseItem.Id,
            CaseNumber = caseItem.CaseNumber,
            CaseType = caseItem.CaseType,
            Status = caseItem.Status,
            CreatedAt = caseItem.CreatedAt,
            UpdatedAt = caseItem.UpdatedAt,
            ApplicantId = caseItem.ApplicantId
        };

        return Created($"/api/cases/{caseItem.Id}", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCases()
    {
        var cases = await _context.Cases
            .Select(c => new CaseResponseDto
            {
                Id = c.Id,
                CaseNumber = c.CaseNumber,
                CaseType = c.CaseType,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                ApplicantId = c.ApplicantId
            })
            .ToListAsync();

        return Ok(cases);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCaseById(Guid id)
    {
        var caseItem = await _context.Cases
            .Where(c => c.Id == id)
            .Select(c => new CaseResponseDto
            {
                Id = c.Id,
                CaseNumber = c.CaseNumber,
                CaseType = c.CaseType,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                ApplicantId = c.ApplicantId
            })
            .FirstOrDefaultAsync();

        if (caseItem == null)
        {
            return NotFound(new
            {
                message = "Case not found."
            });
        }

        return Ok(caseItem);
    }

    [HttpPatch("{id:guid}/status")]
public async Task<IActionResult> UpdateCaseStatus(
    Guid id,
    UpdateCaseStatusDto dto)
{
    var caseItem = await _context.Cases.FindAsync(id);

    if (caseItem == null)
    {
        return NotFound(new
        {
            message = "Case not found."
        });
    }

    if (!_workflowService.CanTransition(caseItem.Status, dto.Status))
    {
        return BadRequest(new
        {
            message = $"Cannot change case status from {caseItem.Status} to {dto.Status}."
        });
    }

    caseItem.Status = dto.Status;
    caseItem.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return NoContent();
}
}
