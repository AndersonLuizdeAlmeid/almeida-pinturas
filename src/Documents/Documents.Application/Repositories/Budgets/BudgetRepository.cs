using Documents.Infrastructure.Data;
using Documents.Infrastructure.Domain;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Repositories.Budgets;
public class BudgetRepository : IBudgetRepository
{
    private readonly IMongoCollection<Budget> _documents;

    public BudgetRepository(MongoDbContext context)
    {
        _documents = context.Budgets;
    }

    public async Task<List<Budget>> GetAllAsync()
        => await _documents.Find(_ => true).ToListAsync();

    public async Task<Budget?> GetByIdAsync(string id)
        => await _documents.Find(d => d.Id == id).FirstOrDefaultAsync();

    public async Task<List<Budget>?> GetByFolderIdAsync(string budgetId)
    => await _documents.Find(d => d.Id == budgetId).ToListAsync();

    public async Task CreateAsync(Budget document)
        => await _documents.InsertOneAsync(document);

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _documents.DeleteOneAsync(d => d.Id == id);
        return result.DeletedCount > 0;
    }
}