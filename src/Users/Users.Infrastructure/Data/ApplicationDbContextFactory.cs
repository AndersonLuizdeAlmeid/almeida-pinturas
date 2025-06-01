using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Users.Infrastructure.Data;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Configure sua connection string aqui
        optionsBuilder.UseSqlServer("Server=45.10.154.254,1433;Database=UsersDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
