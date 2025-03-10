using CSharpFunctionalExtensions;
using Users.Application.Users.Commands;

namespace Users.Application.Users.Handlers;
public interface IUserHandler
{
    Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(ChangeUserCommand command, CancellationToken cancellationToken);
}