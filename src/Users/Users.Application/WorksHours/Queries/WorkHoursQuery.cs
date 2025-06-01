using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Data;

namespace Users.Application.WorksHours.Queries;
public class WorkHoursQuery : IWorkHoursQuery
{
    private readonly ApplicationDbContext _context;

    public WorkHoursQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkHours?> GetByIdAsync(long id)
        => await _context.WorkHours.FindAsync(id);

    public async Task<List<WorkHours>> GetByLocationIdAsync(long locationId)
    {
        return await _context.WorkHours
            .Where(x => x.LocationId == locationId)
            .ToListAsync();
    }
}