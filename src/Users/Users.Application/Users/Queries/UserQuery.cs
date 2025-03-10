#nullable enable
using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Data;

namespace Users.Application.Users.Queries;
public class UserQuery : IUserQuery
{
    private readonly ApplicationDbContext _context;

    public UserQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(long id)
        => await _context.Users.FindAsync(id);

    public async Task<List<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();
    public async Task<List<User>> GetByNameAsync(string name)
        => await _context.Users
            .Where(u => u.Name.Contains(name))
            .ToListAsync();

    public async Task<User> GetByNameAndPassword(string email, string password)
    {
        var user = await _context.Users
          .Where(u => u.Email == email)
          .FirstOrDefaultAsync();

        if (user == null)
            return null;

        bool senhaValida = BCrypt.Net.BCrypt.Verify(password, user.Password);

        return senhaValida ? user : null;
    }
}