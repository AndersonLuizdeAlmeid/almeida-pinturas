#nullable enable
using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Documents.Application.Repositories.Folders;
using Documents.Application.Repositories.Documents;

namespace Documents.Application.Services;
public class DocumentService(IDocumentRepository _documentRepository, 
                             IFolderRepository _folderRepository) : IDocumentService
{
    public async Task<List<Document>> GetAllDocumentsAsync() => await _documentRepository.GetAllAsync();

    public async Task<Document?> GetDocumentByIdAsync(string id) => await _documentRepository.GetByIdAsync(id);

    public async Task<bool> CreateDocumentAsync(Document document)
    {
        var folder = await _folderRepository.GetByIdAsync(document.FolderId);
        if (folder == null) 
            return false;

        await _documentRepository.CreateAsync(document);

        return true;
    }
    public async Task<bool> DeleteDocumentAsync(string id) => await _documentRepository.DeleteAsync(id);
}
