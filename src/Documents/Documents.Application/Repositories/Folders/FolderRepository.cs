using Documents.Infrastructure.Data;
using Documents.Infrastructure.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Application.Repositories.Folders;
public class FolderRepository : IFolderRepository
{
    private readonly IMongoCollection<Folder> _folders;

    public FolderRepository(MongoDbContext context)
    {
        _folders = context.Folders;
    }

    public async Task<List<Folder>> GetAllAsync()
        => await _folders.Find(_ => true).ToListAsync();

    public async Task<Folder?> GetByIdAsync(string folderId)
        => await _folders.Find(f => f.Id == folderId).FirstOrDefaultAsync();

    public async Task CreateAsync(Folder folder)
        => await _folders.InsertOneAsync(folder);
    public async Task<bool> UpdateAsync(Folder folder)
    {
        var result = await _folders.ReplaceOneAsync(f => f.Id == folder.Id, folder);
        return result.ModifiedCount > 0;
    }
}