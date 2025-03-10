using Documents.Application.Domain;

namespace Documents.Application.Repositories;
public interface IDocumentRepository
{
    Task<List<Document>> GetAllAsync();
    Task<Document?> GetByIdAsync(string id);
    Task CreateAsync(Document document);
    Task<bool> DeleteAsync(string id);
}
