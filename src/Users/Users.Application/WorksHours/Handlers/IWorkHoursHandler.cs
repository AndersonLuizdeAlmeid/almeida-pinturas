using CSharpFunctionalExtensions;
using Users.Application.WorksHours.Commands;

namespace Users.Application.WorksHours.Handlers;
public interface IWorkHoursHandler
{
    Task<Result> Handle(DeleteWorkHoursCommand request, CancellationToken cancellationToken);
    Task<Result> Handle(CreateWorkHoursCommand request, CancellationToken cancellationToken);
}