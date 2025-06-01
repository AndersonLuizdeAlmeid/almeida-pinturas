using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Users.Infrastructure.Data;
public class Location
{
    public long Id { get; set; }
    public string Description { get; set; }
    public short IsFinished { get; set; }

    public ICollection<WorkHours> WorkHours { get; set; } = new List<WorkHours>();
}