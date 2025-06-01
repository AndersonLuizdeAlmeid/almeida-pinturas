using Users.Infrastructure.Data;

namespace Users.Application.WorksHours.Queries;
public interface IWorkHoursQuery
{
    Task<WorkHours?> GetByIdAsync(long id);
    Task<List<WorkHours>> GetByLocationIdAsync(long locationId);
}