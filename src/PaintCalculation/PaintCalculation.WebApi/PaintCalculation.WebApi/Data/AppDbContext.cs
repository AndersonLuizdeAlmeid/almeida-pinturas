using Microsoft.EntityFrameworkCore;
using PaintCalculation.WebApi.Models;

namespace PaintCalculation.WebApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<WallSegment> WallSegments => Set<WallSegment>();
}
