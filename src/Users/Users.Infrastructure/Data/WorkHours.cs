using System;
using System.Text.Json.Serialization;

namespace Users.Infrastructure.Data;
public class WorkHours
{
    public long Id { get; set; }
    public long LocationId { get; set; }
    public short Hours { get; set; }
    public DateTime Date {  get; set; }

    [JsonIgnore]
    public Location Location { get; set; }
}