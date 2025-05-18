using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Repositories.Budgets;
public interface IBudgetRepository
{
    Task<List<Budget>> GetAllAsync();
    Task<Budget?> GetByIdAsync(string id);
    Task<List<Budget>?> GetByFolderIdAsync(string userIdid);
    Task CreateAsync(Budget document);
    Task<bool> DeleteAsync(string id);
}