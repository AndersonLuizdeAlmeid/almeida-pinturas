namespace PaintCalculation.WebApi.Models;
public class WallSegment
{
    public int Id { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Area => Width * Height;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}