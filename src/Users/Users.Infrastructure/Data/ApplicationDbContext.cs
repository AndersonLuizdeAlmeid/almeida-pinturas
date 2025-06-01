using Microsoft.EntityFrameworkCore;

namespace Users.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<WorkHours> WorkHours { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Location>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<WorkHours>()
            .HasKey(w => w.Id);

        modelBuilder.Entity<WorkHours>()
            .HasOne(w => w.Location)           
            .WithMany(l => l.WorkHours)        
            .HasForeignKey(w => w.LocationId)  
            .OnDelete(DeleteBehavior.Cascade);

    }

}