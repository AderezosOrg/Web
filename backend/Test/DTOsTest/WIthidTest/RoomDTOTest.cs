using Xunit;
using DTOs.WithId;
using System;
using System.Collections.Generic;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class RoomDTOTest
    {
        [Fact]
        public void RoomDTO_CanSet_RoomID()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var roomId = Guid.NewGuid();

            // Act
            roomDTO.RoomID = roomId;

            // Assert
            Assert.Equal(roomId, roomDTO.RoomID);
        }

        [Fact]
        public void RoomDTO_CanGet_RoomID()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var roomDTO = new RoomDTO { RoomID = roomId };

            // Act & Assert
            Assert.Equal(roomId, roomDTO.RoomID);
        }

        [Fact]
        public void RoomDTO_CanSet_Code()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var code = "R101";

            // Act
            roomDTO.Code = code;

            // Assert
            Assert.Equal(code, roomDTO.Code);
        }

        [Fact]
        public void RoomDTO_CanGet_Code()
        {
            // Arrange
            var code = "R101";
            var roomDTO = new RoomDTO { Code = code };

            // Act & Assert
            Assert.Equal(code, roomDTO.Code);
        }

        [Fact]
        public void RoomDTO_CanSet_FloorNumber()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var floorNumber = 2;

            // Act
            roomDTO.FloorNumber = floorNumber;

            // Assert
            Assert.Equal(floorNumber, roomDTO.FloorNumber);
        }

        [Fact]
        public void RoomDTO_CanGet_FloorNumber()
        {
            // Arrange
            var floorNumber = 2;
            var roomDTO = new RoomDTO { FloorNumber = floorNumber };

            // Act & Assert
            Assert.Equal(floorNumber, roomDTO.FloorNumber);
        }

        [Fact]
        public void RoomDTO_CanSet_PricePerNight()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var price = 150.00m;

            // Act
            roomDTO.PricePerNight = price;

            // Assert
            Assert.Equal(price, roomDTO.PricePerNight);
        }

        [Fact]
        public void RoomDTO_CanGet_PricePerNight()
        {
            // Arrange
            var price = 150.00m;
            var roomDTO = new RoomDTO { PricePerNight = price };

            // Act & Assert
            Assert.Equal(price, roomDTO.PricePerNight);
        }

        [Fact]
        public void RoomDTO_CanSet_RoomTemplateProperties()
        {
            // Arrange
            var roomDTO = new RoomDTO
            {
                RoomTemplateSide = "North",
                RoomTemplateWindows = 2
            };

            // Act & Assert
            Assert.Equal("North", roomDTO.RoomTemplateSide);
            Assert.Equal(2, roomDTO.RoomTemplateWindows);
        }

        [Fact]
        public void RoomDTO_CanSet_BedsList()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var bedList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            roomDTO.Beds = bedList;

            // Assert
            Assert.Equal(bedList, roomDTO.Beds);
        }

        [Fact]
        public void RoomDTO_CanSet_BathroomsList()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var bathroomList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            roomDTO.Bathrooms = bathroomList;

            // Assert
            Assert.Equal(bathroomList, roomDTO.Bathrooms);
        }

        [Fact]
        public void RoomDTO_CanSet_ServicesList()
        {
            // Arrange
            var roomDTO = new RoomDTO();
            var serviceList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            roomDTO.Services = serviceList;

            // Assert
            Assert.Equal(serviceList, roomDTO.Services);
        }

        [Fact]
        public void RoomDTO_CanSet_HotelProperties()
        {
            // Arrange
            var roomDTO = new RoomDTO
            {
                HotelName = "Hotel Test",
                HotelAllowsPets = true
            };

            // Act & Assert
            Assert.Equal("Hotel Test", roomDTO.HotelName);
            Assert.True(roomDTO.HotelAllowsPets);
        }

        [Fact]
        public void RoomDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var roomDTO = new RoomDTO();

            // Assert
            Assert.Equal(Guid.Empty, roomDTO.RoomID);
            Assert.Null(roomDTO.Code);
            Assert.Equal(0, roomDTO.FloorNumber);
            Assert.Equal(0m, roomDTO.PricePerNight);
            Assert.Null(roomDTO.RoomTemplateSide);
            Assert.Equal(0, roomDTO.RoomTemplateWindows);
            Assert.Null(roomDTO.Beds);
            Assert.Null(roomDTO.Bathrooms);
            Assert.Null(roomDTO.Services);
            Assert.Null(roomDTO.HotelName);
            Assert.False(roomDTO.HotelAllowsPets);
        }
    }
}
