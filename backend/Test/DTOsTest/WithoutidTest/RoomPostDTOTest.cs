using System;
using System.Collections.Generic;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class RoomPostDTOTest
    {
        [Fact]
        public void RoomPostDTO_CanSet_Code()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            string code = "Room101";

            // Act
            roomPostDTO.Code = code;

            // Assert
            Assert.Equal(code, roomPostDTO.Code);
        }

        [Fact]
        public void RoomPostDTO_CanGet_Code()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { Code = "Room102" };

            // Act & Assert
            Assert.Equal("Room102", roomPostDTO.Code);
        }

        [Fact]
        public void RoomPostDTO_CanSet_FloorNumber()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            int floorNumber = 2;

            // Act
            roomPostDTO.FloorNumber = floorNumber;

            // Assert
            Assert.Equal(floorNumber, roomPostDTO.FloorNumber);
        }

        [Fact]
        public void RoomPostDTO_CanGet_FloorNumber()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { FloorNumber = 3 };

            // Act & Assert
            Assert.Equal(3, roomPostDTO.FloorNumber);
        }

        [Fact]
        public void RoomPostDTO_CanSet_PricePerNight()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            decimal pricePerNight = 89.99m;

            // Act
            roomPostDTO.PricePerNight = pricePerNight;

            // Assert
            Assert.Equal(pricePerNight, roomPostDTO.PricePerNight);
        }

        [Fact]
        public void RoomPostDTO_CanGet_PricePerNight()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { PricePerNight = 79.99m };

            // Act & Assert
            Assert.Equal(79.99m, roomPostDTO.PricePerNight);
        }

        [Fact]
        public void RoomPostDTO_CanSet_RoomTemplateSide()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            string roomTemplateSide = "North";

            // Act
            roomPostDTO.RoomTemplateSide = roomTemplateSide;

            // Assert
            Assert.Equal(roomTemplateSide, roomPostDTO.RoomTemplateSide);
        }

        [Fact]
        public void RoomPostDTO_CanGet_RoomTemplateSide()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { RoomTemplateSide = "South" };

            // Act & Assert
            Assert.Equal("South", roomPostDTO.RoomTemplateSide);
        }

        [Fact]
        public void RoomPostDTO_CanSet_RoomTemplateWindows()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            int roomTemplateWindows = 2;

            // Act
            roomPostDTO.RoomTemplateWindows = roomTemplateWindows;

            // Assert
            Assert.Equal(roomTemplateWindows, roomPostDTO.RoomTemplateWindows);
        }

        [Fact]
        public void RoomPostDTO_CanGet_RoomTemplateWindows()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { RoomTemplateWindows = 1 };

            // Act & Assert
            Assert.Equal(1, roomPostDTO.RoomTemplateWindows);
        }

        [Fact]
        public void RoomPostDTO_BedsList_Initialized_AsNull()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();

            // Act & Assert
            Assert.Null(roomPostDTO.Beds);
        }

        [Fact]
        public void RoomPostDTO_BedsList_CanSet_Initialized_And_Contain_Guids()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO
            {
                Beds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            // Act & Assert
            Assert.NotNull(roomPostDTO.Beds);
            Assert.Equal(2, roomPostDTO.Beds.Count);
        }

        [Fact]
        public void RoomPostDTO_BathroomsList_Initialized_AsNull()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();

            // Act & Assert
            Assert.Null(roomPostDTO.Bathrooms);
        }

        [Fact]
        public void RoomPostDTO_BathroomsList_CanSet_Initialized_And_Contain_Guids()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO
            {
                Bathrooms = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            // Act & Assert
            Assert.NotNull(roomPostDTO.Bathrooms);
            Assert.Equal(2, roomPostDTO.Bathrooms.Count);
        }

        [Fact]
        public void RoomPostDTO_ServicesList_Initialized_AsNull()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();

            // Act & Assert
            Assert.Null(roomPostDTO.Services);
        }

        [Fact]
        public void RoomPostDTO_ServicesList_CanSet_Initialized_And_Contain_Guids()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO
            {
                Services = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            // Act & Assert
            Assert.NotNull(roomPostDTO.Services);
            Assert.Equal(2, roomPostDTO.Services.Count);
        }

        [Fact]
        public void RoomPostDTO_CanSet_HotelName()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            string hotelName = "Grand Hotel";

            // Act
            roomPostDTO.HotelName = hotelName;

            // Assert
            Assert.Equal(hotelName, roomPostDTO.HotelName);
        }

        [Fact]
        public void RoomPostDTO_CanGet_HotelName()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { HotelName = "Luxury Suites" };

            // Act & Assert
            Assert.Equal("Luxury Suites", roomPostDTO.HotelName);
        }

        [Fact]
        public void RoomPostDTO_CanSet_HotelAllowsPets()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO();
            bool allowsPets = true;

            // Act
            roomPostDTO.HotelAllowsPets = allowsPets;

            // Assert
            Assert.Equal(allowsPets, roomPostDTO.HotelAllowsPets);
        }

        [Fact]
        public void RoomPostDTO_CanGet_HotelAllowsPets()
        {
            // Arrange
            var roomPostDTO = new RoomPostDTO { HotelAllowsPets = false };

            // Act & Assert
            Assert.False(roomPostDTO.HotelAllowsPets);
        }

        [Fact]
        public void RoomPostDTO_DefaultValues_AreNullOrZero()
        {
            // Arrange & Act
            var roomPostDTO = new RoomPostDTO();

            // Assert
            Assert.Null(roomPostDTO.Code);
            Assert.Equal(0, roomPostDTO.FloorNumber);
            Assert.Equal(0m, roomPostDTO.PricePerNight);
            Assert.Null(roomPostDTO.RoomTemplateSide);
            Assert.Equal(0, roomPostDTO.RoomTemplateWindows);
            Assert.Null(roomPostDTO.Beds);
            Assert.Null(roomPostDTO.Bathrooms);
            Assert.Null(roomPostDTO.Services);
            Assert.Null(roomPostDTO.HotelName);
            Assert.False(roomPostDTO.HotelAllowsPets);
        }
    }
}
