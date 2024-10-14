using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class RoomServicesTest
    {
        [Fact]
        public void RoomServices_CanSet_RoomID()
        {
            // Arrange
            var roomService = new RoomServices();
            var roomId = Guid.NewGuid();

            // Act
            roomService.RoomID = roomId;

            // Assert
            Assert.Equal(roomId, roomService.RoomID);
        }

        [Fact]
        public void RoomServices_CanGet_RoomID()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var roomService = new RoomServices { RoomID = roomId };

            // Act & Assert
            Assert.Equal(roomId, roomService.RoomID);
        }

        [Fact]
        public void RoomServices_CanSet_ServiceID()
        {
            // Arrange
            var roomService = new RoomServices();
            var serviceId = Guid.NewGuid();

            // Act
            roomService.ServiceID = serviceId;

            // Assert
            Assert.Equal(serviceId, roomService.ServiceID);
        }

        [Fact]
        public void RoomServices_CanGet_ServiceID()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var roomService = new RoomServices { ServiceID = serviceId };

            // Act & Assert
            Assert.Equal(serviceId, roomService.ServiceID);
        }

        [Fact]
        public void RoomServices_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var roomService = new RoomServices();

            // Assert
            Assert.Equal(Guid.Empty, roomService.RoomID);
            Assert.Equal(Guid.Empty, roomService.ServiceID);
        }
    }
}