using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithIdTest;

public class SessionDTOTests
{
    [Fact]
    public void SessionDTO_Should_Set_And_Get_SessionId()
    {
        // Arrange
        var sessionDto = new SessionDTO();
        var sessionId = Guid.NewGuid();

        // Act
        sessionDto.SessionId = sessionId;

        // Assert
        Assert.Equal(sessionId, sessionDto.SessionId);
    }

    [Fact]
    public void SessionDTO_Should_Set_And_Get_Token()
    {
        // Arrange
        var sessionDto = new SessionDTO();
        var token = "sampleToken123";

        // Act
        sessionDto.Token = token;

        // Assert
        Assert.Equal(token, sessionDto.Token);
    }

    [Fact]
    public void SessionDTO_Should_Set_And_Get_CreationDate()
    {
        // Arrange
        var sessionDto = new SessionDTO();
        var creationDate = DateTime.UtcNow;

        // Act
        sessionDto.CreationDate = creationDate;

        // Assert
        Assert.Equal(creationDate, sessionDto.CreationDate);
    }

    [Fact]
    public void SessionDTO_CreationDate_Should_Be_Of_DateTime_Type()
    {
        // Arrange
        var sessionDto = new SessionDTO();
        var creationDate = DateTime.UtcNow;

        // Act
        sessionDto.CreationDate = creationDate;

        // Assert
        Assert.IsType<DateTime>(sessionDto.CreationDate);
    }

    [Fact]
    public void SessionDTO_Should_Have_Default_Values()
    {
        // Arrange
        var sessionDto = new SessionDTO();

        // Assert
        Assert.Equal(Guid.Empty, sessionDto.SessionId);
        Assert.Null(sessionDto.Token);
        Assert.Equal(default(DateTime), sessionDto.CreationDate);
    }
}