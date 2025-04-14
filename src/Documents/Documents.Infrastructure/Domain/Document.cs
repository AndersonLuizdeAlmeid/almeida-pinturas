using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Documents.Infrastructure.Domain;
public class Document
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Gera um ID único como string
    public string FolderId { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string FileType { get; set; } = null!;
    public long Size { get; set; }
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    public DateTime ExpirateDate { get; set; }
    public byte[] Content { get; set; }
}