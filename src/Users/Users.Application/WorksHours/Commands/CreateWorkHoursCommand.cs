using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.WorksHours.Commands;
public class CreateWorkHoursCommand : IRequest<Result>
{
    public WorkHours WorkHours { get; set; }

    public CreateWorkHoursCommand(WorkHours workHours)
    {
        WorkHours = workHours;
    }
}