using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Documents.Infrastructure.Domain;
public class Folder
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Gera um ID único como string
    public long UserId { get; set; } 
    public string FolderName { get; set; } = null!;
}