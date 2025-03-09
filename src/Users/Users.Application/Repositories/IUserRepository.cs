using Users.Infrastructure.Data;

namespace Users.Application.Repositories;
public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}