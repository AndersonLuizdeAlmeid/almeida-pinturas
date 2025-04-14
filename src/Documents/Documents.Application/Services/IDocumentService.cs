#nullable enable
using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Services;
public interface IDocumentService
{
    Task<List<Document>> GetAllDocumentsAsync();
    Task<Document?> GetDocumentByIdAsync(string id);
    Task<List<DocumentDto>?> GetDocumentByUserIdAsync(long userId);
    Task<bool> CreateDocumentAsync(Document document, long userId);
    Task<bool> DeleteDocumentAsync(string id);
}