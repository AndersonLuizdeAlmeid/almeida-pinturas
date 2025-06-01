using Users.Infrastructure.Data;

namespace Users.Application.Locations.Dtos;
public class LocationDto
{
    public long Id { get; set; }
    public string Description { get; set; }
    public short IsFinished { get; set; }

    public ICollection<WorkHours> WorkHours { get; set; } = new List<WorkHours>();
}