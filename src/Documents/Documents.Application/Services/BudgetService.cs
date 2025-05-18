using Documents.Application.Repositories.Budgets;
using Documents.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Documents.Application.Services;
public class BudgetService(IBudgetRepository _budgetRepository) : IBudgetService
{
    public async Task<List<BudgetDto>> GetAllBudgetsAsync()
    {
        var budget = await _budgetRepository.GetAllAsync();
        return budget == null ? null : budget.Select(doc => new BudgetDto
        {
            Id = doc.Id,
            Name = doc.Name,
            DateCreated = doc.DateCreated,
            Content = $"data:pdf;base64,{Convert.ToBase64String(doc.Content)}",
        }).ToList();
    }

    public async Task<BudgetDto?> GetBudgetByIdAsync(string id)
    {
        var budget = await _budgetRepository.GetByIdAsync(id);

        return budget == null ? null :
            new BudgetDto
            {
                Id = budget.Id,
                Name = budget.Name,
                DateCreated = budget.DateCreated,
                Content = $"data:pdf;base64,{Convert.ToBase64String(budget.Content)}",
            };
    }
    public async Task<bool> CreateBudgetAsync(Budget budget)
    {
        try
        {
            await _budgetRepository.CreateAsync(budget);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteBudgetAsync(string id)
        => await _budgetRepository.DeleteAsync(id);
}