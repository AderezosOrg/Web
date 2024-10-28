using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Services;
using backend.Services.ServicesInterfaces;
using Db;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;

public class SessionServiceTests
{
    private readonly Mock<IDAO<Session>> _sessionDaoMock;
    private readonly Mock<IDAO<Contact>> _contactDaoMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly SessionService _sessionService;

    public SessionServiceTests()
    {
        _sessionDaoMock = new Mock<IDAO<Session>>();
        _contactDaoMock = new Mock<IDAO<Contact>>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _sessionService = new SessionService(
            _httpContextAccessorMock.Object,
            _sessionDaoMock.Object,
            _contactDaoMock.Object
        );
    }

    [Fact]
    public async Task PostSession_ValidSessionPostDTO_ReturnsSessionFullInfoDTO()
    {
        // Arrange
        var sessionPostDto = new SessionPostDTO { Email = "test@example.com", Token = "dummyToken" };

        // Act
        var result = await _sessionService.PostSession(sessionPostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sessionPostDto.Email, result.Email);
        Assert.Equal(sessionPostDto.Token, result.Token);
        _contactDaoMock.Verify(x => x.Create(It.IsAny<Contact>()), Times.Once);
        _sessionDaoMock.Verify(x => x.Create(It.IsAny<Session>()), Times.Once);
    }

    [Fact]
    public async Task GetCookie_ValidSessionId_AddsCookie()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new Session { SessionID = sessionId, Token = "iujdafsoiadasdh34t4jkoxcjlxvcxvxvdkocswojdqijacvzxvdidjc", ContactID = Guid.NewGuid() };
        var contact = new Contact { ContactID = session.ContactID, Email = "test@example.com", PhoneNumber = "123456789" };

        _sessionDaoMock.Setup(x => x.Read(sessionId)).Returns(session);
        _contactDaoMock.Setup(x => x.Read(session.ContactID)).Returns(contact);

        var httpContext = new DefaultHttpContext();
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = await _sessionService.GetCookie(sessionId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateSession_ValidSessionFullInfoDTO_ReturnsUpdatedSessionFullInfoDTO()
    {
        // Arrange
        var sessionFullInfoDto = new SessionFullInfoDTO
        {
            SessionID = Guid.NewGuid(),
            ContactID = Guid.NewGuid(),
            Email = "updated@example.com",
            PhoneNumber = "987654321",
            Token = "updatedToken"
        };

        var oldSession = new Session
        {
            SessionID = sessionFullInfoDto.SessionID,
            ContactID = sessionFullInfoDto.ContactID,
            Token = "oldToken",
            CreationDate = DateTime.Now.AddDays(-1)
        };

        _sessionDaoMock.Setup(x => x.Read(sessionFullInfoDto.SessionID)).Returns(oldSession);

        // Act
        var result = await _sessionService.UpdateSession(sessionFullInfoDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sessionFullInfoDto.Email, result.Email);
        Assert.Equal(sessionFullInfoDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(sessionFullInfoDto.Token, result.Token);
        _sessionDaoMock.Verify(x => x.Update(It.IsAny<Session>()), Times.Once);
        _contactDaoMock.Verify(x => x.Update(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task RefreshToken_ValidSessionDTO_ReturnsUpdatedSessionFullInfoDTO()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var sessionDto = new SessionDTO { SessionId = sessionId, Token = "newTokenkgvhjbnkiuytgfhijku8y76tfgniku8tyfgnikutyfgnhik" };
        var oldSession = new Session { SessionID = sessionId, ContactID = Guid.NewGuid(), Token = "oldToken1wdfgy654dfgyujfrtgujhyiutrfgvujhbyitrfuj", CreationDate = DateTime.Now.AddDays(-1) };
        var contact = new Contact { ContactID = oldSession.ContactID, Email = "test@example.com", PhoneNumber = "123456789" };

        _sessionDaoMock.Setup(x => x.Read(sessionDto.SessionId)).Returns(oldSession);
        _contactDaoMock.Setup(x => x.Read(oldSession.ContactID)).Returns(contact);

        // Act
        var result = await _sessionService.RefreshToken(sessionDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contact.Email, result.Email);
        Assert.Equal(sessionDto.Token, result.Token);
        _sessionDaoMock.Verify(x => x.Update(It.IsAny<Session>()), Times.Once);
    }

    [Fact]
    public async Task GetSession_ValidSessionId_ReturnsSessionFullInfoDTO()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new Session { SessionID = sessionId, Token = "dummyToken", ContactID = Guid.NewGuid(), CreationDate = DateTime.Now };
        var contact = new Contact { ContactID = session.ContactID, Email = "test@example.com", PhoneNumber = "123456789" };

        _sessionDaoMock.Setup(x => x.Read(sessionId)).Returns(session);
        _contactDaoMock.Setup(x => x.Read(session.ContactID)).Returns(contact);

        // Act
        var result = await _sessionService.GetSession(sessionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sessionId, result.SessionID);
        Assert.Equal(contact.Email, result.Email);
        Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
    }

    [Fact]
    public async Task GetSessions_ReturnsListOfSessionDTO()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new Session { SessionID = Guid.NewGuid(), Token = "token1", CreationDate = DateTime.Now },
            new Session { SessionID = Guid.NewGuid(), Token = "token2", CreationDate = DateTime.Now }
        };

        _sessionDaoMock.Setup(x => x.ReadAll()).Returns(sessions);

        // Act
        var result = await _sessionService.GetSessions();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sessions.Count, result.Count);
    }

    [Fact]
    public async Task IsTokenValid_ValidSessionId_ReturnsTrueIfNotExpired()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new Session { SessionID = sessionId, CreationDate = DateTime.Now };
        _sessionDaoMock.Setup(x => x.Read(sessionId)).Returns(session);

        // Act
        var result = await _sessionService.IsTokenValid(sessionId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsTokenValid_ExpiredSessionId_ReturnsFalse()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new Session { SessionID = sessionId, CreationDate = DateTime.Now.AddMinutes(-31) };
        _sessionDaoMock.Setup(x => x.Read(sessionId)).Returns(session);

        // Act
        var result = await _sessionService.IsTokenValid(sessionId);

        // Assert
        Assert.False(result);
    }
}
