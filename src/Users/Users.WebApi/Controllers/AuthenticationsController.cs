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
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [Authorization]
    public IActionResult ValidateToken()
    {
        return Ok();
    }
}