using Xunit;
using DTOs.WithId;
using System;
namespace backend.Test.DTOsTest.WithIdTest;

public class BathroomAddToTemplateDTOTests
{
    [Fact]
    public void BathroomAddToTemplateDTO_CanSetProperties()
    {
        // Arrange
        var bathroomDTO = new BathroomAddToTemplateDTO();
        var bathroomId = Guid.NewGuid();
        int bathroomQuantity = 3;

        // Act
        bathroomDTO.BathRoomID = bathroomId;
        bathroomDTO.BathroomQuantity = bathroomQuantity;

        // Assert
        Assert.Equal(bathroomId, bathroomDTO.BathRoomID);
        Assert.Equal(bathroomQuantity, bathroomDTO.BathroomQuantity);
    }

    [Fact]
    public void BathroomAddToTemplateDTO_DefaultConstructor_SetsPropertiesToDefault()
    {
        // Arrange & Act
        var bathroomDTO = new BathroomAddToTemplateDTO();

        // Assert
        Assert.Equal(Guid.Empty, bathroomDTO.BathRoomID);
        Assert.Equal(0, bathroomDTO.BathroomQuantity);
    }

    [Fact]
    public void BathroomAddToTemplateDTO_CanHandleNegativeBathroomQuantity()
    {
        // Arrange
        var bathroomDTO = new BathroomAddToTemplateDTO();
        int negativeQuantity = -1;

        // Act
        bathroomDTO.BathroomQuantity = negativeQuantity;

        // Assert
        Assert.Equal(negativeQuantity, bathroomDTO.BathroomQuantity);
    }

    [Fact]
    public void BathroomAddToTemplateDTO_CanHandleLargeBathroomQuantity()
    {
        // Arrange
        var bathroomDTO = new BathroomAddToTemplateDTO();
        int largeQuantity = int.MaxValue;

        // Act
        bathroomDTO.BathroomQuantity = largeQuantity;

        // Assert
        Assert.Equal(largeQuantity, bathroomDTO.BathroomQuantity);
    }
}
