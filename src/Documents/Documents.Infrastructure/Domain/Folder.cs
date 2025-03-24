using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;

namespace Documents.Infrastructure.Domain;
public class Folder
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Gera um ID único como string
    public string UserId { get; set; } = null!;
    public string FolderName { get; set; } = null!;
}