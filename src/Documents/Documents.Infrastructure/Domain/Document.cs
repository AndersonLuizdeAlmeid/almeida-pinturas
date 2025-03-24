using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Documents.Infrastructure.Domain;
public class Document
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string FileType { get; set; } = null!;
    public long Size { get; set; } // Tamanho do arquivo em bytes
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    public string StorageUrl { get; set; } = null!;
}