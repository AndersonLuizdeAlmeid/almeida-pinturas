using CSharpFunctionalExtensions;
using Users.Application.Commands;

namespace Users.Application.Handlers;
public interface IUserHandler
{
    Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(ChangeUserCommand command, CancellationToken cancellationToken);
}