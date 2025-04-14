using Documents.Infrastructure.Domain.Enums;
using System;

namespace Documents.Infrastructure.Domain;
public class DocumentDto
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string Content { get; set; }
    public TypeEnum Type { get; set; }
    public DateTime ExpirationDate { get; set; }
}