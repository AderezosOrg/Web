using backend.Services;
using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;

public class UserServiceTests
{
    private readonly Mock<IDAO<User>> _mockUserDAO;
    private readonly Mock<IDAO<Contact>> _mockContactDAO;
    private readonly UserService _userService;
    
    public UserServiceTests()
    {
        _mockUserDAO = new Mock<IDAO<User>>();
        _mockContactDAO = new Mock<IDAO<Contact>>();
        _userService = new UserService(_mockUserDAO.Object, _mockContactDAO.Object);
    }

    [Fact]
    public async Task GetElementById_Returns_UserPostDTO_When_UserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var user = new User
        {
            UserID = userId,
            Name = "John",
            CINumber = "1234",
            ContactID = contactId
        };
        var contact = new Contact { ContactID = contactId, Email = "test@example.com" };

        _mockUserDAO.Setup(x => x.Read(userId)).Returns(user);
        _mockContactDAO.Setup(x => x.Read(contactId)).Returns(contact);

        // Act
        var result = await _userService.GetElementById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(contact.ContactID, result.ContactId);
    }

    [Fact]
    public async Task GetElementById_ThrowsException_When_UserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockUserDAO.Setup(x => x.Read(userId)).Returns((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetElementById(userId));
    }

    [Fact]
    public async Task GetAllElements_Returns_ListOfUserDTOs()
    {
        // Arrange
        var users = new List<User>
        {
            new User { UserID = Guid.NewGuid(), Name = "User1", ContactID = Guid.NewGuid() },
            new User { UserID = Guid.NewGuid(), Name = "User2", ContactID = Guid.NewGuid() }
        };
        var contacts = new List<Contact>
        {
            new Contact { ContactID = users[0].ContactID, Email = "user1@example.com" },
            new Contact { ContactID = users[1].ContactID, Email = "user2@example.com" }
        };

        _mockUserDAO.Setup(x => x.ReadAll()).Returns(users);
        _mockContactDAO.Setup(x => x.Read(It.IsAny<Guid>())).Returns<Guid>(id => contacts.FirstOrDefault(c => c.ContactID == id));

        // Act
        var result = await _userService.GetAllElements();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(users[0].Name, result[0].Name);
        Assert.Equal(users[1].Name, result[1].Name);
    }

    [Fact]
    public async Task CreateSingleElement_CreatesAndReturns_UserDTO()
    {
        // Arrange
        var userPostDto = new UserPostDTO
        {
            Name = "New User",
            CINumber = "5678",
            ContactId = Guid.NewGuid()
        };

        var newUser = new User
        {
            UserID = Guid.NewGuid(),
            Name = userPostDto.Name,
            CINumber = userPostDto.CINumber,
            ContactID = userPostDto.ContactId
        };
        var contact = new Contact { ContactID = newUser.ContactID, Email = "newuser@example.com" };

        _mockUserDAO.Setup(x => x.Create(It.IsAny<User>())).Verifiable();
        _mockContactDAO.Setup(x => x.Read(userPostDto.ContactId)).Returns(contact);

        // Act
        var result = await _userService.CreateSingleElement(userPostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userPostDto.Name, result.Name);
        _mockUserDAO.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task UpdateElementById_UpdatesAndReturns_UserPostDTO()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var oldUser = new User
        {
            UserID = userId,
            Name = "Old Name",
            CINumber = "1234",
            ContactID = Guid.NewGuid()
        };
        var updatedUserDto = new UpdateUserDTO { Name = "Updated Name", CINumber = "5678" };
        var contact = new Contact { ContactID = oldUser.ContactID, Email = "updateduser@example.com" };

        _mockUserDAO.Setup(x => x.Read(userId)).Returns(oldUser);
        _mockUserDAO.Setup(x => x.Update(It.IsAny<User>())).Verifiable();
        _mockContactDAO.Setup(x => x.Read(oldUser.ContactID)).Returns(contact);

        // Act
        var result = await _userService.UpdateElementById(userId, updatedUserDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedUserDto.Name, result.Name);
        _mockUserDAO.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task DeleteElementById_Returns_True_When_DeletedSuccessfully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockUserDAO.Setup(x => x.Delete(userId)).Returns(true);

        // Act
        var result = await _userService.DeleteElementById(userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteElementById_Returns_False_When_DeleteFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockUserDAO.Setup(x => x.Delete(userId)).Returns(false);

        // Act
        var result = await _userService.DeleteElementById(userId);

        // Assert
        Assert.False(result);
    }
}
