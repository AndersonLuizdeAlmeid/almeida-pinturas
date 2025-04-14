#nullable enable
using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Documents.Application.Repositories.Folders;
using Documents.Application.Repositories.Documents;
using System.Linq;
using System;
using Documents.Infrastructure.Utils;

namespace Documents.Application.Services;
public class DocumentService(IDocumentRepository _documentRepository, 
                             IFolderRepository _folderRepository) : IDocumentService
{
    public async Task<List<Document>> GetAllDocumentsAsync() => await _documentRepository.GetAllAsync();

    public async Task<Document?> GetDocumentByIdAsync(string id) => await _documentRepository.GetByIdAsync(id);
    public async Task<List<DocumentDto>?> GetDocumentByUserIdAsync(long userId)
    {
        var folder = await _folderRepository.GetByUserIdAsync(userId);
        if (folder == null)
            return null;

        var files = await _documentRepository.GetByFolderIdAsync(folder.Id);

        return files == null ? null : files.Select(doc => new DocumentDto
        {
            Id = doc.Id,
            FileName = doc.FileName,
            FileType = doc.FileType,
            Content = $"data:{doc.FileType};base64,{Convert.ToBase64String(doc.Content)}",
            Type = SetTypeDate.SetType(doc.ExpirateDate),
            ExpirationDate = doc.ExpirateDate
        }).ToList();

    }

    public async Task<bool> CreateDocumentAsync(Document document, long userId)
    {
        var folder = await _folderRepository.GetByUserIdAsync(userId);
        if (folder == null) 
            return false;

        document.FolderId = folder.Id;

        await _documentRepository.CreateAsync(document);

        return true;
    }

    public async Task<bool> UploadDocumentAsync(Document document)
    {
        var folder = await _folderRepository.GetByIdAsync(document.FolderId);
        if (folder == null)
            return false;

        await _documentRepository.CreateAsync(document);

        return true;
    }

    public async Task<bool> DeleteDocumentAsync(string id) => await _documentRepository.DeleteAsync(id);
}
