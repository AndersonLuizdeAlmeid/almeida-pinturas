using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Users.Application.Authentications.JwtToken.Services;

namespace Users.WebApi.Authorizations;
public class AuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? token = context.HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value;

        if (token.IsNullOrEmpty())
        {
            context.Result = new ObjectResult("Token inválido.") { StatusCode = (int)HttpStatusCode.Unauthorized };
            return;
        }

        if (token!.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            token = token.Substring(7);

        var decryptedToken = TokenService.DecryptToken(token!);

        if ((decryptedToken.ExpirationDate - DateTime.UtcNow).TotalMinutes <= 0)
        {
            context.Result = new ObjectResult("Token expirado.") { StatusCode = (int)HttpStatusCode.Unauthorized };
            return;
        }
        context.HttpContext.Items["UserID"] = decryptedToken.UserID;
    }

}