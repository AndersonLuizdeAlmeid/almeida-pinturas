using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Data;

namespace Users.Application.Locations.Queries;
public class LocationQuery : ILocationQuery
{
    private readonly ApplicationDbContext _context;

    public LocationQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Location?> GetByIdAsync(long id)
        => await _context.Locations.FindAsync(id);

    public async Task<List<Location>> GetAllAsync()
        => await _context.Locations
            .Include(l => l.WorkHours)
            .ToListAsync();
}