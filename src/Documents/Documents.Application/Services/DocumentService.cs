#nullable enable
using Documents.Infrastructure.Domain;
using Documents.Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Services;
public class DocumentService(IDocumentRepository _documentRepository) : IDocumentService
{
    public async Task<List<Document>> GetAllDocumentsAsync() => await _documentRepository.GetAllAsync();

    public async Task<Document?> GetDocumentByIdAsync(string id) => await _documentRepository.GetByIdAsync(id);

    public async Task CreateDocumentAsync(Document document) => await _documentRepository.CreateAsync(document);

    public async Task<bool> DeleteDocumentAsync(string id) => await _documentRepository.DeleteAsync(id);
}
