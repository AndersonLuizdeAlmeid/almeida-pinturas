using Users.Infrastructure.Data;

namespace Users.Application.Locations.Repositories;
public interface ILocationRepository
{
    Task AddAsync(Location location, CancellationToken cancellationToken);
    Task ChangeAsync(Location location, CancellationToken cancellationToken);
    Task DeleteAsync(Location location, CancellationToken cancellationToken);
}