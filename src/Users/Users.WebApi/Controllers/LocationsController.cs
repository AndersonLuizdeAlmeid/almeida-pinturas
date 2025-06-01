using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Locations.Commands;
using Users.Application.Locations.Queries;
using Users.WebApi.Authorizations;

namespace Users.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class LocationsController(ILocationQuery _locationQuery, IMediator _mediator) : ControllerBase
{
    [HttpGet]
    [Authorization]
    public async Task<IActionResult> GetAll()
       => Ok(await _locationQuery.GetAllAsync());

    [HttpPost]
    [Authorization]
    public async Task<IActionResult> Create([FromBody] CreateLocationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(new { message = result.Error });

            return Ok(new { message = "Localização inserida com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorization]
    public async Task<IActionResult> Update([FromBody] ChangeLocationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(new { message = result.Error });

            return Ok(new { message = "Localização alterada com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorization]
    public async Task<IActionResult> Delete([FromBody] DeleteLocationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(new { message = result.Error });

            return Ok(new { message = "Localização excluída com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}