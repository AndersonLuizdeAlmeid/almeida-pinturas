using Microsoft.EntityFrameworkCore;
using Moq;
using Users.Application.Repositories;
using Users.Infrastructure.Data;

namespace Users.UnitTesting.Application.Repositories;
public class UserRepositoryTest
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepositoryTest()
    {
        // Usando InMemoryDatabase para simular o banco de dados
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "MeuBanco")
            .Options;

        _dbContext = new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepository_WhenCalled()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John Doe" };
        var repositoryMock = new Mock<IUserRepository>();

        repositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        await repositoryMock.Object.AddAsync(user);

        // Assert
        repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUser_WhenCalled()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John Doe" };
        var repositoryMock = new Mock<IUserRepository>();

        repositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);


        // Act
        user.Name = "Jane Doe";
        await repositoryMock.Object.UpdateAsync(user);

        // Assert
        repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
    }
}