using Xunit;
using Converters.ToDTO;
using System;
using System.Collections.Generic;
using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithIdTest;

public class RoomTemplateDTOTests
{
    [Fact]
    public void RoomTemplateDTO_CanSetProperties()
    {
        // Arrange
        var roomTemplateDTO = new RoomTemplateDTO();
        var roomTemplateId = Guid.NewGuid();
        var beds = new List<BedDTO> { new BedDTO { BedID = Guid.NewGuid(), BedQuantity = 2 } };
        var bathrooms = new List<BathroomDTO> { new BathroomDTO { BathRoomID = Guid.NewGuid(), BathroomQuantity = 1 } };

        // Act
        roomTemplateDTO.RoomTemplateID = roomTemplateId;
        roomTemplateDTO.Side = "North";
        roomTemplateDTO.Windows = 3;
        roomTemplateDTO.Beds = beds;
        roomTemplateDTO.Bathrooms = bathrooms;

        // Assert
        Assert.Equal(roomTemplateId, roomTemplateDTO.RoomTemplateID);
        Assert.Equal("North", roomTemplateDTO.Side);
        Assert.Equal(3, roomTemplateDTO.Windows);
        Assert.Equal(beds, roomTemplateDTO.Beds);
        Assert.Equal(bathrooms, roomTemplateDTO.Bathrooms);
    }

    [Fact]
    public void RoomTemplateDTO_DefaultConstructor_SetsPropertiesToDefault()
    {
        // Arrange & Act
        var roomTemplateDTO = new RoomTemplateDTO();

        // Assert
        Assert.Equal(Guid.Empty, roomTemplateDTO.RoomTemplateID);
        Assert.Null(roomTemplateDTO.Side);
        Assert.Equal(0, roomTemplateDTO.Windows);
        Assert.Null(roomTemplateDTO.Beds);
        Assert.Null(roomTemplateDTO.Bathrooms);
    }

    [Fact]
    public void RoomTemplateDTO_CanHandleEmptyCollections()
    {
        // Arrange
        var roomTemplateDTO = new RoomTemplateDTO();
        var emptyBeds = new List<BedDTO>();
        var emptyBathrooms = new List<BathroomDTO>();

        // Act
        roomTemplateDTO.Beds = emptyBeds;
        roomTemplateDTO.Bathrooms = emptyBathrooms;

        // Assert
        Assert.Empty(roomTemplateDTO.Beds);
        Assert.Empty(roomTemplateDTO.Bathrooms);
    }
}