using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.WorksHours.Commands;
public class DeleteWorkHoursCommand : IRequest<Result>
{
    public WorkHours WorkHours { get; set; }

    public DeleteWorkHoursCommand(WorkHours workHours)
    {
        WorkHours = workHours;
    }
}