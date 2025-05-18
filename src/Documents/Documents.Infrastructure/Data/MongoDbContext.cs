using Documents.Infrastructure.Domain;
using Documents.Infrastructure.Data.MongoSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Documents.Infrastructure.Data;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<Document> Documents => _database.GetCollection<Document>("Documents");
    public IMongoCollection<Folder> Folders => _database.GetCollection<Folder>("Folders");
    public IMongoCollection<Budget> Budgets => _database.GetCollection<Budget>("Budgets");
}