using Xunit;
using DTOs.WithId;
using System;
namespace backend.Test.DTOsTest.WithIdTest;

public class BedAddToTemplateDTOTests
{
    [Fact]
    public void BedAddToTemplateDTO_CanSetProperties()
    {
        // Arrange
        var bedDTO = new BedAddToTemplateDTO();
        var bedId = Guid.NewGuid();
        int bedQuantity = 5;

        // Act
        bedDTO.BedID = bedId;
        bedDTO.BedQuantity = bedQuantity;

        // Assert
        Assert.Equal(bedId, bedDTO.BedID);
        Assert.Equal(bedQuantity, bedDTO.BedQuantity);
    }

    [Fact]
    public void BedAddToTemplateDTO_DefaultConstructor_SetsPropertiesToDefault()
    {
        // Arrange & Act
        var bedDTO = new BedAddToTemplateDTO();

        // Assert
        Assert.Equal(Guid.Empty, bedDTO.BedID);
        Assert.Equal(0, bedDTO.BedQuantity);
    }

    [Fact]
    public void BedAddToTemplateDTO_CanHandleNegativeBedQuantity()
    {
        // Arrange
        var bedDTO = new BedAddToTemplateDTO();
        int negativeQuantity = -2;

        // Act
        bedDTO.BedQuantity = negativeQuantity;

        // Assert
        Assert.Equal(negativeQuantity, bedDTO.BedQuantity);
    }

    [Fact]
    public void BedAddToTemplateDTO_CanHandleLargeBedQuantity()
    {
        // Arrange
        var bedDTO = new BedAddToTemplateDTO();
        int largeQuantity = int.MaxValue;

        // Act
        bedDTO.BedQuantity = largeQuantity;

        // Assert
        Assert.Equal(largeQuantity, bedDTO.BedQuantity);
    }
}