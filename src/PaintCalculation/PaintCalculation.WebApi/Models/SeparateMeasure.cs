using System.Text.Json.Serialization;

namespace PaintCalculation.WebApi.Models;
public class SeparateMeasure
{
    public long Id { get; set; }
    public long MeasureId { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    [JsonIgnore]
    public Measure? Measure { get; set; } // navegação opcional
}