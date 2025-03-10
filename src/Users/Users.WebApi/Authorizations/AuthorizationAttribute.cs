using Microsoft.AspNetCore.Mvc;

namespace Users.WebApi.Authorizations;
public class AuthorizationAttribute : TypeFilterAttribute
{
    public AuthorizationAttribute() : base(typeof(AuthorizationFilter)) { }
}