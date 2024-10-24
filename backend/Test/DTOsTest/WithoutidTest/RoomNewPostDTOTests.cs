using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class RoomNewPostDTOTests
{
    [Fact]
    public void RoomNewPostDTO_Should_Set_And_Get_Code()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var code = "R001";

        // Act
        roomDto.Code = code;

        // Assert
        Assert.Equal(code, roomDto.Code);
    }

    [Fact]
    public void RoomNewPostDTO_Should_Set_And_Get_FloorNumber()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var floorNumber = 3;

        // Act
        roomDto.FloorNumber = floorNumber;

        // Assert
        Assert.Equal(floorNumber, roomDto.FloorNumber);
    }

    [Fact]
    public void RoomNewPostDTO_Should_Set_And_Get_PricePerNight()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var price = 120.50m;

        // Act
        roomDto.PricePerNight = price;

        // Assert
        Assert.Equal(price, roomDto.PricePerNight);
    }

    [Fact]
    public void RoomNewPostDTO_Should_Set_And_Get_RoomTemplateId()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var templateId = Guid.NewGuid();

        // Act
        roomDto.RoomTemplateId = templateId;

        // Assert
        Assert.Equal(templateId, roomDto.RoomTemplateId);
    }

    [Fact]
    public void RoomNewPostDTO_Should_Set_And_Get_HotelId()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var hotelId = Guid.NewGuid();

        // Act
        roomDto.HotelId = hotelId;

        // Assert
        Assert.Equal(hotelId, roomDto.HotelId);
    }

    [Fact]
    public void RoomNewPostDTO_Should_Set_And_Get_RoomServices()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var roomServices = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        roomDto.RoomServices = roomServices;

        // Assert
        Assert.Equal(roomServices, roomDto.RoomServices);
    }

    [Fact]
    public void RoomNewPostDTO_Should_Allow_Empty_RoomServices_List()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();
        var roomServices = new List<Guid>();

        // Act
        roomDto.RoomServices = roomServices;

        // Assert
        Assert.NotNull(roomDto.RoomServices);
        Assert.Empty(roomDto.RoomServices);
    }

    [Fact]
    public void RoomNewPostDTO_RoomServices_Should_Be_Null_By_Default()
    {
        // Arrange
        var roomDto = new RoomNewPostDTO();

        // Assert
        Assert.Null(roomDto.RoomServices);
    }
}
