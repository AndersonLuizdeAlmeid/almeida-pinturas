using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Documents.Infrastructure.Domain;
public class Budget
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Gera um ID único como string
    public string Name { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public byte[] Content { get; set; }
}