using Microsoft.EntityFrameworkCore;
using PaintCalculation.WebApi.Models;

namespace PaintCalculation.WebApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Measure> Measure { get; set; }
    public DbSet<SeparateMeasure> SeparateMeasure { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measure>()
            .HasMany(m => m.Separates)
            .WithOne(s => s.Measure!)
            .HasForeignKey(s => s.MeasureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
