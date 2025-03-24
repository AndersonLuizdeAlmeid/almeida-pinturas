#nullable enable
using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Services;
public interface IDocumentService
{
    Task<List<Document>> GetAllDocumentsAsync();
    Task<Document?> GetDocumentByIdAsync(string id);
    Task CreateDocumentAsync(Document document);
    Task<bool> DeleteDocumentAsync(string id);
}