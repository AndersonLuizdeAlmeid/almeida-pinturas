#nullable enable
using Users.Infrastructure.Data;

namespace Users.Application.Queries;
public interface IUserQuery
{
    Task<User> GetByIdAsync(long id);
    Task<List<User>> GetAllAsync();
    Task<List<User>> GetByNameAsync(string name);
}