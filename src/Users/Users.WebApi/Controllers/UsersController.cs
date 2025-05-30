using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Commands;
using Users.Application.Users.Queries;
using Users.WebApi.Authorizations;
using Users.WebApi.RabbitMQ;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UsersController(IUserQuery _userQueries, IMediator _mediator, RabbitMQPublisher _rabbitMQPublisher) : ControllerBase
{
    [HttpGet("{id}")]
    [Authorization]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _userQueries.GetByIdAsync(id);

        return user != null ? Ok(user) : NotFound();
    }

    [HttpGet]
    [Authorization]
    public async Task<IActionResult> GetAll()
        => Ok(await _userQueries.GetAllAsync());

    [HttpGet("search")]
    [Authorization]
    public async Task<IActionResult> GetByName([FromQuery] string name) 
        => Ok(await _userQueries.GetByNameAsync(name));

    [HttpPost]
    [Authorization]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(new { message = result.Error });

            _rabbitMQPublisher.PublishUserCreatedEvent(command.User.Id);

            return Ok(new { message = "Usu�rio criado com sucesso!" });
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorization]
    public async Task<IActionResult> Update([FromBody] ChangeUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok("Usu�rio alterado com sucesso!");
    }
}
