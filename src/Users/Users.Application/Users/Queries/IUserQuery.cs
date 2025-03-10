#nullable enable
using Azure.Core;
using System.Threading;
using Users;
using Users.Infrastructure.Data;

namespace Users.Application.Users.Queries;
public interface IUserQuery
{
    Task<User> GetByIdAsync(long id);
    Task<List<User>> GetAllAsync();
    Task<List<User>> GetByNameAsync(string name);
    Task<User> GetByNameAndPassword(string email, string password);
}