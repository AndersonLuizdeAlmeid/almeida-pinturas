namespace PaintCalculation.WebApi.Models;
public class Measure
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Area { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<SeparateMeasure> Separates { get; set; } = new();
}