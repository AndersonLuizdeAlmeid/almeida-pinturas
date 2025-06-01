using Users.Infrastructure.Data;

namespace Users.Application.Locations.Repositories;
public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _context.Locations.AddAsync(location);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeAsync(Location location, CancellationToken cancellationToken)
    {
        _context.Locations.Update(location);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Location location, CancellationToken cancellationToken)
    {
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
    }
}