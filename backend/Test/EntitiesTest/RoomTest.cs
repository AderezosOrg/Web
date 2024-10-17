using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class RoomTest
    {
        [Fact]
        public void Room_CanSet_RoomID()
        {
            // Arrange
            var room = new Room();
            var roomId = Guid.NewGuid();

            // Act
            room.RoomID = roomId;

            // Assert
            Assert.Equal(roomId, room.RoomID);
        }

        [Fact]
        public void Room_CanGet_RoomID()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var room = new Room { RoomID = roomId };

            // Act & Assert
            Assert.Equal(roomId, room.RoomID);
        }

        [Fact]
        public void Room_CanSet_Code()
        {
            // Arrange
            var room = new Room();
            var code = "R101";

            // Act
            room.Code = code;

            // Assert
            Assert.Equal(code, room.Code);
        }

        [Fact]
        public void Room_CanGet_Code()
        {
            // Arrange
            var code = "R101";
            var room = new Room { Code = code };

            // Act & Assert
            Assert.Equal(code, room.Code);
        }

        [Fact]
        public void Room_CanSet_FloorNumber()
        {
            // Arrange
            var room = new Room();
            var floorNumber = 3;

            // Act
            room.FloorNumber = floorNumber;

            // Assert
            Assert.Equal(floorNumber, room.FloorNumber);
        }

        [Fact]
        public void Room_CanGet_FloorNumber()
        {
            // Arrange
            var floorNumber = 3;
            var room = new Room { FloorNumber = floorNumber };

            // Act & Assert
            Assert.Equal(floorNumber, room.FloorNumber);
        }

        [Fact]
        public void Room_CanSet_PricePerNight()
        {
            // Arrange
            var room = new Room();
            var pricePerNight = 99.99m;

            // Act
            room.PricePerNight = pricePerNight;

            // Assert
            Assert.Equal(pricePerNight, room.PricePerNight);
        }

        [Fact]
        public void Room_CanGet_PricePerNight()
        {
            // Arrange
            var pricePerNight = 99.99m;
            var room = new Room { PricePerNight = pricePerNight };

            // Act & Assert
            Assert.Equal(pricePerNight, room.PricePerNight);
        }

        [Fact]
        public void Room_CanSet_RoomTemplateID()
        {
            // Arrange
            var room = new Room();
            var roomTemplateId = Guid.NewGuid();

            // Act
            room.RoomTemplateID = roomTemplateId;

            // Assert
            Assert.Equal(roomTemplateId, room.RoomTemplateID);
        }

        [Fact]
        public void Room_CanGet_RoomTemplateID()
        {
            // Arrange
            var roomTemplateId = Guid.NewGuid();
            var room = new Room { RoomTemplateID = roomTemplateId };

            // Act & Assert
            Assert.Equal(roomTemplateId, room.RoomTemplateID);
        }

        [Fact]
        public void Room_CanSet_HotelID()
        {
            // Arrange
            var room = new Room();
            var hotelId = Guid.NewGuid();

            // Act
            room.HotelID = hotelId;

            // Assert
            Assert.Equal(hotelId, room.HotelID);
        }

        [Fact]
        public void Room_CanGet_HotelID()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var room = new Room { HotelID = hotelId };

            // Act & Assert
            Assert.Equal(hotelId, room.HotelID);
        }

        [Fact]
        public void Room_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var room = new Room();

            // Assert
            Assert.Equal(Guid.Empty, room.RoomID);
            Assert.Null(room.Code);
            Assert.Equal(0, room.FloorNumber);
            Assert.Equal(0m, room.PricePerNight);
            Assert.Equal(Guid.Empty, room.RoomTemplateID);
            Assert.Equal(Guid.Empty, room.HotelID);
        }
    }
}
