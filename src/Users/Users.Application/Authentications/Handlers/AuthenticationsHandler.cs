using CSharpFunctionalExtensions;
using MediatR;
using Users.Application.Authentications.Commands;
using Users.Application.Authentications.JwtToken.Services;
using Users.Application.Users.Queries;
using Users.Infrastructure.Utils;

namespace Users.Application.Authentications.Handlers;
public class AuthenticationsHandler(IUserQuery _userQuery) : IRequestHandler<AuthenticationCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        if(!EmailService.ValidateEmail(request.Email))
            return Result.Failure<string>("Email está com problema.");

        var user = await _userQuery.GetByNameAndPassword(request.Email, request.Password);

        if (user.IsActive == 0)
            return Result.Failure<string>("O usuário está inativo.");
        var isActive = true;

        var token = TokenService.GenerateCustomToken(user.Id, isActive);

        return Result.Success(token);
    }
}