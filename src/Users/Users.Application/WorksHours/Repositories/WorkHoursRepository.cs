using Users.Infrastructure.Data;

namespace Users.Application.WorksHours.Repositories;
public class WorkHoursRepository : IWorkHoursRepository
{
    private readonly ApplicationDbContext _context;

    public WorkHoursRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(WorkHours workHours, CancellationToken cancellationToken)
    {
        await _context.WorkHours.AddAsync(workHours);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(WorkHours workHours, CancellationToken cancellationToken)
    {
        _context.WorkHours.Remove(workHours);
        await _context.SaveChangesAsync();
    }
}