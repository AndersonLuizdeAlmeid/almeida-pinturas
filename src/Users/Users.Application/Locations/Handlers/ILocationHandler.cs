using CSharpFunctionalExtensions;
using Users.Application.Locations.Commands;

namespace Users.Application.Locations.Handlers;
public interface ILocationHandler
{
    Task<Result> Handle(CreateLocationCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(ChangeLocationCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteLocationCommand command, CancellationToken cancellationToken);
}