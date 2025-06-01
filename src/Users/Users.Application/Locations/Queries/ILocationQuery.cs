using Users.Infrastructure.Data;

namespace Users.Application.Locations.Queries;
public interface ILocationQuery
{
    Task<Location?> GetByIdAsync(long id);
    Task<List<Location>> GetAllAsync();
}