using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Users.Application.Commands;
using Users.Application.Queries;
using Users.Infrastructure.Data;

namespace Users.UnitTesting.WebApi.Controllers;
public class UsersControllerTest
{
    private readonly Mock<IUserQuery> _mockUserQuery;
    private readonly Mock<IMediator> _mockMediator;
    private readonly UsersController _controller;

    public UsersControllerTest()
    {
        _mockUserQuery = new Mock<IUserQuery>();
        _mockMediator = new Mock<IMediator>();
        _controller = new UsersController(_mockUserQuery.Object, _mockMediator.Object);
    }

    [Fact]
    public async Task GetById_ShouldReturnOkResult_WhenUserExists()
    {
        // Arrange
        var userId = 1L;
        var user = new User { Id = userId, Name = "John Doe" };
        _mockUserQuery.Setup(q => q.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(userId, returnedUser.Id);
        Assert.Equal("John Doe", returnedUser.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFoundResult_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1L;
        _mockUserQuery.Setup(q => q.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkResult_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Doe" }
            };
        _mockUserQuery.Setup(q => q.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count);
    }

    [Fact]
    public async Task GetByName_ShouldReturnOkResult_WhenUsersExistWithMatchingName()
    {
        // Arrange
        var name = "John";
        var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe" }
            };
        _mockUserQuery.Setup(q => q.GetByNameAsync(name)).ReturnsAsync(users);

        // Act
        var result = await _controller.GetByName(name);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Single(returnedUsers);
        Assert.Equal("John Doe", returnedUsers[0].Name);
    }

    [Fact]
    public async Task Create_ShouldReturnOkResult_WhenCreateUserCommandIsSuccessful()
    {
        // Arrange
        var command = new CreateUserCommand(new User { Name = "John Doe" });
        var result = Result.Success();
        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Create(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Usuário criado com sucesso!", message);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenCreateUserCommandFails()
    {
        // Arrange
        var command = new CreateUserCommand(new User { Name = "John Doe" });
        var result = Result.Failure("Erro ao criar usuário");
        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Create(command, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var error = badRequestResult.Value as dynamic;        
        Assert.Equal("Erro ao criar usuário", error.ToString());
    }

    [Fact]
    public async Task Update_ShouldReturnOkResult_WhenUpdateUserCommandIsSuccessful()
    {
        // Arrange
        var command = new ChangeUserCommand(new User { Id = 1, Name = "Updated Name" });
        var result = Result.Success();
        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Update(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Usuário alterado com sucesso!", message);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenUpdateUserCommandFails()
    {
        // Arrange
        var command = new ChangeUserCommand(new User { Id = 1, Name = "Updated Name" });
        var result = Result.Failure("Erro ao alterar usuário");
        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Update(command, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var error = badRequestResult.Value as dynamic;
        Assert.Equal("Erro ao alterar usuário", error.ToString());
    }
}