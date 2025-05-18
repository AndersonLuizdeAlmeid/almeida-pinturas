using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Services;
public interface IBudgetService
{
    Task<List<BudgetDto>> GetAllBudgetsAsync();
    Task<BudgetDto?> GetBudgetByIdAsync(string id);
    Task<bool> CreateBudgetAsync(Budget budget);
    Task<bool> DeleteBudgetAsync(string id);
}