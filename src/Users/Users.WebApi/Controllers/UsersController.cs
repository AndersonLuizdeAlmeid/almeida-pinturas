using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Users.Application.Commands;
using Users.Application.Queries;
using Users.Application.Repositories;
using Users.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserQuery _userQueries;
    private readonly IMediator _mediator;

    public UsersController(IUserQuery userQueries, IMediator mediator)
    {
        _userQueries = userQueries;
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _userQueries.GetByIdAsync(id);

        return user != null ? Ok(user) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _userQueries.GetAllAsync());

    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name) 
        => Ok(await _userQueries.GetByNameAsync(name));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok("Usuário criado com sucesso!");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ChangeUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok("Usuário alterado com sucesso!");
    }
}
