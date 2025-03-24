#nullable enable
using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Repositories;
public interface IDocumentRepository
{
    Task<List<Document>> GetAllAsync();
    Task<Document?> GetByIdAsync(string id);
    Task CreateAsync(Document document);
    Task<bool> DeleteAsync(string id);
}
