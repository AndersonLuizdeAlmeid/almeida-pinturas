using Documents.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Repositories.Folders;
public interface IFolderRepository
{
    Task<List<Folder>> GetAllAsync();
    Task<Folder?> GetByIdAsync(string folderId);
    Task<Folder> GetByUserIdAsync(long userId);
    Task CreateAsync(Folder folder);
    Task<bool> UpdateAsync(Folder folder);
}