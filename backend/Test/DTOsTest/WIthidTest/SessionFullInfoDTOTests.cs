using System;
using Xunit;
using DTOs.WithId;

namespace backend.Test.DTOsTest.WithIdTest;

public class SessionFullInfoDTOTests
{
    [Fact]
    public void SessionFullInfoDTO_Should_Set_And_Get_SessionID()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();
        var sessionId = Guid.NewGuid();

        // Act
        sessionDto.SessionID = sessionId;

        // Assert
        Assert.Equal(sessionId, sessionDto.SessionID);
    }

    [Fact]
    public void SessionFullInfoDTO_Should_Set_And_Get_Token()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();
        var token = "sampleToken123";

        // Act
        sessionDto.Token = token;

        // Assert
        Assert.Equal(token, sessionDto.Token);
    }

    [Fact]
    public void SessionFullInfoDTO_Should_Set_And_Get_CreationDate()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();
        var creationDate = DateTime.UtcNow;

        // Act
        sessionDto.CreationDate = creationDate;

        // Assert
        Assert.Equal(creationDate, sessionDto.CreationDate);
    }

    [Fact]
    public void SessionFullInfoDTO_Should_Set_And_Get_ContactID()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();
        var contactId = Guid.NewGuid();

        // Act
        sessionDto.ContactID = contactId;

        // Assert
        Assert.Equal(contactId, sessionDto.ContactID);
    }

    [Fact]
    public void SessionFullInfoDTO_Should_Set_And_Get_Email()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();
        var email = "example@mail.com";

        // Act
        sessionDto.Email = email;

        // Assert
        Assert.Equal(email, sessionDto.Email);
    }

    [Fact]
    public void SessionFullInfoDTO_Should_Set_And_Get_PhoneNumber()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();
        var phoneNumber = "123456789";

        // Act
        sessionDto.PhoneNumber = phoneNumber;

        // Assert
        Assert.Equal(phoneNumber, sessionDto.PhoneNumber);
    }

    [Fact]
    public void SessionFullInfoDTO_Should_Have_Default_Values()
    {
        // Arrange
        var sessionDto = new SessionFullInfoDTO();

        // Assert
        Assert.Equal(Guid.Empty, sessionDto.SessionID);
        Assert.Null(sessionDto.Token);
        Assert.Equal(default(DateTime), sessionDto.CreationDate);
        Assert.Equal(Guid.Empty, sessionDto.ContactID);
        Assert.Null(sessionDto.Email);
        Assert.Null(sessionDto.PhoneNumber);
    } 
}
