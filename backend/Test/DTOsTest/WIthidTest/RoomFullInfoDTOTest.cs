using Xunit;
using DTOs.WithId;
using DTOs.WithoutId;
using System;
using System.Collections.Generic;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class RoomFullInfoDTOTest
    {
        [Fact]
        public void RoomFullInfoDTO_CanSet_RoomID()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var roomId = Guid.NewGuid();

            // Act
            roomFullInfoDTO.RoomID = roomId;

            // Assert
            Assert.Equal(roomId, roomFullInfoDTO.RoomID);
        }

        [Fact]
        public void RoomFullInfoDTO_CanGet_RoomID()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var roomFullInfoDTO = new RoomFullInfoDTO { RoomID = roomId };

            // Act & Assert
            Assert.Equal(roomId, roomFullInfoDTO.RoomID);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_Code()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var code = "R101";

            // Act
            roomFullInfoDTO.Code = code;

            // Assert
            Assert.Equal(code, roomFullInfoDTO.Code);
        }

        [Fact]
        public void RoomFullInfoDTO_CanGet_Code()
        {
            // Arrange
            var code = "R101";
            var roomFullInfoDTO = new RoomFullInfoDTO { Code = code };

            // Act & Assert
            Assert.Equal(code, roomFullInfoDTO.Code);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_FloorNumber()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var floorNumber = 2;

            // Act
            roomFullInfoDTO.FloorNumber = floorNumber;

            // Assert
            Assert.Equal(floorNumber, roomFullInfoDTO.FloorNumber);
        }

        [Fact]
        public void RoomFullInfoDTO_CanGet_FloorNumber()
        {
            // Arrange
            var floorNumber = 2;
            var roomFullInfoDTO = new RoomFullInfoDTO { FloorNumber = floorNumber };

            // Act & Assert
            Assert.Equal(floorNumber, roomFullInfoDTO.FloorNumber);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_PricePerNight()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var price = 150.00m;

            // Act
            roomFullInfoDTO.PricePerNight = price;

            // Assert
            Assert.Equal(price, roomFullInfoDTO.PricePerNight);
        }

        [Fact]
        public void RoomFullInfoDTO_CanGet_PricePerNight()
        {
            // Arrange
            var price = 150.00m;
            var roomFullInfoDTO = new RoomFullInfoDTO { PricePerNight = price };

            // Act & Assert
            Assert.Equal(price, roomFullInfoDTO.PricePerNight);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_RoomTemplateProperties()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO
            {
                RoomTemplateSide = "North",
                RoomTemplateWindows = 2
            };

            // Act & Assert
            Assert.Equal("North", roomFullInfoDTO.RoomTemplateSide);
            Assert.Equal(2, roomFullInfoDTO.RoomTemplateWindows);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_BedsList()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var bedList = new List<BedPostDTO>
            {
                new BedPostDTO { Size = "Single", Capacity = "1" },
                new BedPostDTO { Size = "Double", Capacity = "2" }
            };

            // Act
            roomFullInfoDTO.Beds = bedList;

            // Assert
            Assert.Equal(bedList, roomFullInfoDTO.Beds);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_BathroomsList()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var bathroomList = new List<BathroomPostDTO>
            {
                new BathroomPostDTO { Shower = true, Toilet = true, DressingTable = false },
                new BathroomPostDTO { Shower = false, Toilet = true, DressingTable = true }
            };

            // Act
            roomFullInfoDTO.Bathrooms = bathroomList;

            // Assert
            Assert.Equal(bathroomList, roomFullInfoDTO.Bathrooms);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_ServicesList()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO();
            var serviceList = new List<ServicePostDTO>
            {
                new ServicePostDTO { Type = "WiFi" },
                new ServicePostDTO { Type = "Breakfast" }
            };

            // Act
            roomFullInfoDTO.Services = serviceList;

            // Assert
            Assert.Equal(serviceList, roomFullInfoDTO.Services);
        }

        [Fact]
        public void RoomFullInfoDTO_CanSet_HotelProperties()
        {
            // Arrange
            var roomFullInfoDTO = new RoomFullInfoDTO
            {
                HotelName = "Hotel Test",
                HotelAllowsPets = true
            };

            // Act & Assert
            Assert.Equal("Hotel Test", roomFullInfoDTO.HotelName);
            Assert.True(roomFullInfoDTO.HotelAllowsPets);
        }

        [Fact]
        public void RoomFullInfoDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var roomFullInfoDTO = new RoomFullInfoDTO();

            // Assert
            Assert.Equal(Guid.Empty, roomFullInfoDTO.RoomID);
            Assert.Null(roomFullInfoDTO.Code);
            Assert.Equal(0, roomFullInfoDTO.FloorNumber);
            Assert.Equal(0m, roomFullInfoDTO.PricePerNight);
            Assert.Null(roomFullInfoDTO.RoomTemplateSide);
            Assert.Equal(0, roomFullInfoDTO.RoomTemplateWindows);
            Assert.Null(roomFullInfoDTO.Beds);
            Assert.Null(roomFullInfoDTO.Bathrooms);
            Assert.Null(roomFullInfoDTO.Services);
            Assert.Null(roomFullInfoDTO.HotelName);
            Assert.False(roomFullInfoDTO.HotelAllowsPets);
        }
    }
}
