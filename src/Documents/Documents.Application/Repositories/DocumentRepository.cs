using Documents.Application.Domain;
using Documents.Infrastructure.Data;
using MongoDB.Driver;

namespace Documents.Application.Repositories;
public class DocumentRepository : IDocumentRepository
{
    private readonly IMongoCollection<Document> _documents;

    public DocumentRepository(MongoDbContext context)
    {
        _documents = (IMongoCollection<Document>?)context.Documents;
    }

    public async Task<List<Document>> GetAllAsync()
        => await _documents.Find(_ => true).ToListAsync();

    public async Task<Document?> GetByIdAsync(string id)
        => await _documents.Find(d => d.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Document document)
        => await _documents.InsertOneAsync(document);

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _documents.DeleteOneAsync(d => d.Id == id);
        return result.DeletedCount > 0;
    }
}
