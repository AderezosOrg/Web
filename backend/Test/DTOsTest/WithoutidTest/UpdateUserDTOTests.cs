using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class UpdateUserDTOTests
{
    [Fact]
    public void UpdateUserDTO_Should_Set_And_Get_Name()
    {
        // Arrange
        var updateUser = new UpdateUserDTO();
        var name = "John Doe";

        // Act
        updateUser.Name = name;

        // Assert
        Assert.Equal(name, updateUser.Name);
    }

    [Fact]
    public void UpdateUserDTO_Should_Set_And_Get_CINumber()
    {
        // Arrange
        var updateUser = new UpdateUserDTO();
        var ciNumber = "12345678";

        // Act
        updateUser.CINumber = ciNumber;

        // Assert
        Assert.Equal(ciNumber, updateUser.CINumber);
    }

    [Fact]
    public void UpdateUserDTO_Name_Should_Be_Null_By_Default()
    {
        // Arrange
        var updateUser = new UpdateUserDTO();

        // Assert
        Assert.Null(updateUser.Name);
    }

    [Fact]
    public void UpdateUserDTO_CINumber_Should_Be_Null_By_Default()
    {
        // Arrange
        var updateUser = new UpdateUserDTO();

        // Assert
        Assert.Null(updateUser.CINumber);
    }
}
