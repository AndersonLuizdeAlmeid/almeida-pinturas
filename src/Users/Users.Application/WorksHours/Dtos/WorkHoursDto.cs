namespace Users.Application.WorksHours.Dtos;
public class WorkHoursDto
{
    public long Id { get; set; }
    public long LocationId { get; set; }
    public short Hours { get; set; }
    public DateTime Date { get; set; }
}