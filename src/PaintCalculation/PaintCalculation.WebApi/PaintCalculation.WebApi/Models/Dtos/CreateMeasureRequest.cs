namespace PaintCalculation.WebApi.Models.Dtos;
public class CreateMeasureRequest
{
    public string Description { get; set; } = string.Empty;
    public double Area {  get; set; }
    public List<SeparateMeasureRequest> Separates { get; set; } = new();
}