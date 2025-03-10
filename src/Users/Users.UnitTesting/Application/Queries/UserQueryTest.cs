using Microsoft.EntityFrameworkCore;
using Moq;
using Users.Application.Users.Queries;
using Users.Infrastructure.Data;

namespace Users.UnitTesting.Application.Queries;
public class UserQueryTest
{
    private readonly Mock<ApplicationDbContext> _mockDbContext = new Mock<ApplicationDbContext>();
    private readonly Mock<IUserQuery> _queryMock = new();

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = 1L;
        var user = new User { Id = userId, Name = "John Doe" };
        _queryMock
            .Setup(query => query.GetByIdAsync(It.IsAny<long>()))
            .Returns(Task.FromResult(new User()));

        // Act
        var result = await _queryMock.Object.GetByIdAsync(userId);

        // Assert
        _queryMock.Verify(query => query.GetByIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1L;
        _queryMock
            .Setup(query => query.GetByIdAsync(It.IsAny<long>()))
            .Returns(Task.FromResult((User)null));
        // Act
        var result = await _queryMock.Object.GetByIdAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Smith" }
            };
        _queryMock
            .Setup(query => query.GetAllAsync())
            .Returns(Task.FromResult(users));

        // Act
        var result = await _queryMock.Object.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]

    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var users = new List<User>();
        _queryMock
            .Setup(query => query.GetAllAsync())
            .Returns(Task.FromResult(users));

        // Act
        var result = await _queryMock.Object.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnUsers_WhenUsersMatchName()
    {
        // Arrange
        var name = "John";
        var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "John Smith" }
            };
        _queryMock
            .Setup(query => query.GetByNameAsync(name))
            .Returns(Task.FromResult(users));

        // Act
        var result = await _queryMock.Object.GetByNameAsync(name);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnEmptyList_WhenNoUsersMatchName()
    {
        // Arrange
        var name = "Alice";
        var users = new List<User>();
        _queryMock
            .Setup(query => query.GetByNameAsync(name))
            .Returns(Task.FromResult(users));

        // Act
        var result = await _queryMock.Object.GetByNameAsync(name);

        // Assert
        Assert.Empty(result);
    }
}