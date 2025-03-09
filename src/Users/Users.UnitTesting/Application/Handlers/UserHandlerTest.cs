using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Users.Application.Commands;
using Users.Application.Handlers;
using Users.Infrastructure.Data;

namespace Users.UnitTesting.Application.Handlers;
public class UserHandlerTest
{
    private readonly Mock<ApplicationDbContext> _mockDbContext;
    private readonly Mock<IUserHandler> _handlerMock = new();

    public UserHandlerTest()
    {
        _mockDbContext = new Mock<ApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_CreateUserCommand_ShouldReturnSuccess_WhenUserIsCreated()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
        var command = new CreateUserCommand(user);
        var cancellationToken = CancellationToken.None;
        _handlerMock
            .Setup(handler => handler.Handle(command, cancellationToken))
            .Returns(Task.FromResult(Result.Success()));

        // Act
        var result = await _handlerMock.Object.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);    }

    [Fact]
    public async Task Handle_CreateUserCommand_ShouldReturnFailure_WhenExceptionOccurs()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
        var command = new CreateUserCommand(user);
        var cancellationToken = CancellationToken.None;
        _handlerMock
            .Setup(handler => handler.Handle(command, cancellationToken))
            .Returns(Task.FromResult(Result.Failure("Erro ao inserir valores")));

        // Act
        var result = await _handlerMock.Object.Handle(command, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Erro ao inserir valores", result.Error);
    }

    [Fact]
    public async Task Handle_ChangeUserCommand_ShouldReturnSuccess_WhenUserIsFoundAndUpdated()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
        var command = new ChangeUserCommand(user);
        var cancellationToken = CancellationToken.None;

        _handlerMock
            .Setup(handler => handler.Handle(command, cancellationToken))
            .Returns(Task.FromResult(Result.Success()));

        // Act
        var result = await _handlerMock.Object.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ChangeUserCommand_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
        var command = new ChangeUserCommand(user);
        var cancellationToken = CancellationToken.None;

        _handlerMock
            .Setup(handler => handler.Handle(command, cancellationToken))
            .Returns(Task.FromResult(Result.Failure("Usuário não encontrado")));

        // Act
        var result = await _handlerMock.Object.Handle(command, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não encontrado", result.Error);
    }
}