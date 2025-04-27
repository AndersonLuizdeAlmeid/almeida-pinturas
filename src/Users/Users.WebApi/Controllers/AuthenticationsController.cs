using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Authentications.Commands;
using Users.WebApi.Authorizations;

namespace Users.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationsController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(new { token = result.Value });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorization]
    public IActionResult ValidateToken()
    {
        return Ok();
    }
}