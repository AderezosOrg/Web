using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class SessionPostDTOTests
{
    [Fact]
    public void SessionPostDTO_Should_Set_And_Get_Email()
    {
        // Arrange
        var sessionPost = new SessionPostDTO();
        var email = "test@example.com";

        // Act
        sessionPost.Email = email;

        // Assert
        Assert.Equal(email, sessionPost.Email);
    }

    [Fact]
    public void SessionPostDTO_Should_Set_And_Get_Token()
    {
        // Arrange
        var sessionPost = new SessionPostDTO();
        var token = "abc123token";

        // Act
        sessionPost.Token = token;

        // Assert
        Assert.Equal(token, sessionPost.Token);
    }

    [Fact]
    public void SessionPostDTO_Email_Should_Be_Null_By_Default()
    {
        // Arrange
        var sessionPost = new SessionPostDTO();

        // Assert
        Assert.Null(sessionPost.Email);
    }

    [Fact]
    public void SessionPostDTO_Token_Should_Be_Null_By_Default()
    {
        // Arrange
        var sessionPost = new SessionPostDTO();

        // Assert
        Assert.Null(sessionPost.Token);
    }
}