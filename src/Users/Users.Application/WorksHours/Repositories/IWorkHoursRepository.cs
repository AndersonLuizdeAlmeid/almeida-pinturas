using Users.Infrastructure.Data;

namespace Users.Application.WorksHours.Repositories;
public interface IWorkHoursRepository
{
    Task AddAsync(WorkHours workHours, CancellationToken cancellationToken);
    Task DeleteAsync(WorkHours workHours, CancellationToken cancellationToken);
}