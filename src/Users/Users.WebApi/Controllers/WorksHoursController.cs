using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.WorksHours.Commands;
using Users.Application.WorksHours.Queries;
using Users.WebApi.Authorizations;

namespace Users.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class WorksHoursController(IWorkHoursQuery _workHoursQuery, IMediator _mediator) : ControllerBase
{
    [HttpGet("{locationId}")]
    [Authorization]
    public async Task<IActionResult> GetAll(long locationId)
       => Ok(await _workHoursQuery.GetByLocationIdAsync(locationId));

    [HttpPost]
    [Authorization]
    public async Task<IActionResult> Create([FromBody] CreateWorkHoursCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(new { message = result.Error });

            return Ok(new { message = "Horário inserido com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorization]
    public async Task<IActionResult> Delete([FromBody] DeleteWorkHoursCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(new { message = result.Error });

            return Ok(new { message = "Horário excluido com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}