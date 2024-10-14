using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class HotelDTOTest
    {
        [Fact]
        public void HotelDTO_CanSet_HotelID()
        {
            // Arrange
            var hotelDTO = new HotelDTO();
            var hotelId = Guid.NewGuid();

            // Act
            hotelDTO.HotelID = hotelId;

            // Assert
            Assert.Equal(hotelId, hotelDTO.HotelID);
        }

        [Fact]
        public void HotelDTO_CanGet_HotelID()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotelDTO = new HotelDTO { HotelID = hotelId };

            // Act & Assert
            Assert.Equal(hotelId, hotelDTO.HotelID);
        }

        [Fact]
        public void HotelDTO_CanSet_Stars()
        {
            // Arrange
            var hotelDTO = new HotelDTO();

            // Act
            hotelDTO.Stars = 5;

            // Assert
            Assert.Equal(5, hotelDTO.Stars);
        }

        [Fact]
        public void HotelDTO_CanGet_Stars()
        {
            // Arrange
            var hotelDTO = new HotelDTO { Stars = 4 };

            // Act & Assert
            Assert.Equal(4, hotelDTO.Stars);
        }

        [Fact]
        public void HotelDTO_CanSet_Name()
        {
            // Arrange
            var hotelDTO = new HotelDTO();
            var name = "Hotel Test";

            // Act
            hotelDTO.Name = name;

            // Assert
            Assert.Equal(name, hotelDTO.Name);
        }

        [Fact]
        public void HotelDTO_CanGet_Name()
        {
            // Arrange
            var hotelDTO = new HotelDTO { Name = "Another Hotel" };

            // Act & Assert
            Assert.Equal("Another Hotel", hotelDTO.Name);
        }

        [Fact]
        public void HotelDTO_CanSet_AllowsPets()
        {
            // Arrange
            var hotelDTO = new HotelDTO();

            // Act
            hotelDTO.AllowsPets = true;

            // Assert
            Assert.True(hotelDTO.AllowsPets);
        }

        [Fact]
        public void HotelDTO_CanGet_AllowsPets()
        {
            // Arrange
            var hotelDTO = new HotelDTO { AllowsPets = false };

            // Act & Assert
            Assert.False(hotelDTO.AllowsPets);
        }

        [Fact]
        public void HotelDTO_CanSet_Address()
        {
            // Arrange
            var hotelDTO = new HotelDTO();
            var address = "123 Test St";

            // Act
            hotelDTO.Address = address;

            // Assert
            Assert.Equal(address, hotelDTO.Address);
        }

        [Fact]
        public void HotelDTO_CanGet_Address()
        {
            // Arrange
            var hotelDTO = new HotelDTO { Address = "456 Another St" };

            // Act & Assert
            Assert.Equal("456 Another St", hotelDTO.Address);
        }

        [Fact]
        public void HotelDTO_CanSet_UserProperties()
        {
            // Arrange
            var hotelDTO = new HotelDTO
            {
                UserName = "John Doe",
                UserCINumber = "C12345678",
                UserPhoneNumber = "123-456-7890",
                UserEmail = "johndoe@example.com"
            };

            // Act & Assert
            Assert.Equal("John Doe", hotelDTO.UserName);
            Assert.Equal("C12345678", hotelDTO.UserCINumber);
            Assert.Equal("123-456-7890", hotelDTO.UserPhoneNumber);
            Assert.Equal("johndoe@example.com", hotelDTO.UserEmail);
        }

        [Fact]
        public void HotelDTO_CanSet_ContactProperties()
        {
            // Arrange
            var hotelDTO = new HotelDTO
            {
                HotelPhoneNumber = "098-765-4321",
                HotelEmail = "hotel@example.com"
            };

            // Act & Assert
            Assert.Equal("098-765-4321", hotelDTO.HotelPhoneNumber);
            Assert.Equal("hotel@example.com", hotelDTO.HotelEmail);
        }

        [Fact]
        public void HotelDTO_CanSet_BathroomProperties()
        {
            // Arrange
            var hotelDTO = new HotelDTO
            {
                Shower = true,
                Toilet = true,
                DressingTable = false
            };

            // Act & Assert
            Assert.True(hotelDTO.Shower);
            Assert.True(hotelDTO.Toilet);
            Assert.False(hotelDTO.DressingTable);
        }

        [Fact]
        public void HotelDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var hotelDTO = new HotelDTO();

            // Assert
            Assert.Equal(Guid.Empty, hotelDTO.HotelID);
            Assert.Equal(0, hotelDTO.Stars);
            Assert.Null(hotelDTO.Name);
            Assert.False(hotelDTO.AllowsPets);
            Assert.Null(hotelDTO.Address);
            Assert.Null(hotelDTO.UserName);
            Assert.Null(hotelDTO.UserCINumber);
            Assert.Null(hotelDTO.UserPhoneNumber);
            Assert.Null(hotelDTO.UserEmail);
            Assert.Null(hotelDTO.HotelPhoneNumber);
            Assert.Null(hotelDTO.HotelEmail);
            Assert.False(hotelDTO.Shower);
            Assert.False(hotelDTO.Toilet);
            Assert.False(hotelDTO.DressingTable);
        }
    }
}
