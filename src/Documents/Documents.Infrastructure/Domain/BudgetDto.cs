using System;

namespace Documents.Infrastructure.Domain;

public class BudgetDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string Content { get; set; }
}