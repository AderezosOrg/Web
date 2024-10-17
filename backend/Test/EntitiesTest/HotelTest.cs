using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class HotelTest
    {
        [Fact]
        public void Hotel_CanSet_HotelID()
        {
            // Arrange
            var hotel = new Hotel();
            var hotelId = Guid.NewGuid();

            // Act
            hotel.HotelID = hotelId;

            // Assert
            Assert.Equal(hotelId, hotel.HotelID);
        }

        [Fact]
        public void Hotel_CanGet_HotelID()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel { HotelID = hotelId };

            // Act & Assert
            Assert.Equal(hotelId, hotel.HotelID);
        }

        [Fact]
        public void Hotel_CanSet_Stars()
        {
            // Arrange
            var hotel = new Hotel();
            var stars = 5;

            // Act
            hotel.Stars = stars;

            // Assert
            Assert.Equal(stars, hotel.Stars);
        }

        [Fact]
        public void Hotel_CanGet_Stars()
        {
            // Arrange
            var stars = 3;
            var hotel = new Hotel { Stars = stars };

            // Act & Assert
            Assert.Equal(stars, hotel.Stars);
        }

        [Fact]
        public void Hotel_CanSet_Name()
        {
            // Arrange
            var hotel = new Hotel();
            var name = "Hotel Paradise";

            // Act
            hotel.Name = name;

            // Assert
            Assert.Equal(name, hotel.Name);
        }

        [Fact]
        public void Hotel_CanGet_Name()
        {
            // Arrange
            var name = "Luxury Stay";
            var hotel = new Hotel { Name = name };

            // Act & Assert
            Assert.Equal(name, hotel.Name);
        }

        [Fact]
        public void Hotel_CanSet_AllowsPets()
        {
            // Arrange
            var hotel = new Hotel();

            // Act
            hotel.AllowsPets = true;

            // Assert
            Assert.True(hotel.AllowsPets);
        }

        [Fact]
        public void Hotel_CanGet_AllowsPets()
        {
            // Arrange
            var hotel = new Hotel { AllowsPets = false };

            // Act & Assert
            Assert.False(hotel.AllowsPets);
        }

        [Fact]
        public void Hotel_CanSet_Address()
        {
            // Arrange
            var hotel = new Hotel();
            var address = "1234 Elm Street";

            // Act
            hotel.Address = address;

            // Assert
            Assert.Equal(address, hotel.Address);
        }

        [Fact]
        public void Hotel_CanGet_Address()
        {
            // Arrange
            var address = "5678 Oak Avenue";
            var hotel = new Hotel { Address = address };

            // Act & Assert
            Assert.Equal(address, hotel.Address);
        }

        [Fact]
        public void Hotel_CanSet_UserID()
        {
            // Arrange
            var hotel = new Hotel();
            var userId = Guid.NewGuid();

            // Act
            hotel.UserID = userId;

            // Assert
            Assert.Equal(userId, hotel.UserID);
        }

        [Fact]
        public void Hotel_CanGet_UserID()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var hotel = new Hotel { UserID = userId };

            // Act & Assert
            Assert.Equal(userId, hotel.UserID);
        }

        [Fact]
        public void Hotel_CanSet_ContactID()
        {
            // Arrange
            var hotel = new Hotel();
            var contactId = Guid.NewGuid();

            // Act
            hotel.ContactID = contactId;

            // Assert
            Assert.Equal(contactId, hotel.ContactID);
        }

        [Fact]
        public void Hotel_CanGet_ContactID()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var hotel = new Hotel { ContactID = contactId };

            // Act & Assert
            Assert.Equal(contactId, hotel.ContactID);
        }

        [Fact]
        public void Hotel_CanSet_BathRoomID()
        {
            // Arrange
            var hotel = new Hotel();
            var bathRoomId = Guid.NewGuid();

            // Act
            hotel.BathRoomID = bathRoomId;

            // Assert
            Assert.Equal(bathRoomId, hotel.BathRoomID);
        }

        [Fact]
        public void Hotel_CanGet_BathRoomID()
        {
            // Arrange
            var bathRoomId = Guid.NewGuid();
            var hotel = new Hotel { BathRoomID = bathRoomId };

            // Act & Assert
            Assert.Equal(bathRoomId, hotel.BathRoomID);
        }

        [Fact]
        public void Hotel_DefaultValues_AreZeroOrDefault()
        {
            // Arrange & Act
            var hotel = new Hotel();

            // Assert
            Assert.Equal(0, hotel.Stars);
            Assert.Null(hotel.Name);
            Assert.Null(hotel.Address);
            Assert.Equal(Guid.Empty, hotel.UserID);
            Assert.Equal(Guid.Empty, hotel.ContactID);
            Assert.Equal(Guid.Empty, hotel.BathRoomID);
            Assert.False(hotel.AllowsPets);
        }

    }
}