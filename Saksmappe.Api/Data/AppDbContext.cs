using Microsoft.EntityFrameworkCore;
using Saksmappe.Api.Models;

namespace Saksmappe.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Case> Cases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Case>()
            .Property(c => c.CaseType)
            .HasConversion<string>();

        modelBuilder.Entity<Case>()
            .Property(c => c.Status)
            .HasConversion<string>();
    }
}