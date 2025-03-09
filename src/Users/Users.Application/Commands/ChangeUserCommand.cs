using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.Commands;
public class ChangeUserCommand : IRequest<Result>
{
    public User User { get; set; }

    public ChangeUserCommand(User user)
    {
        User = user;
    }
}