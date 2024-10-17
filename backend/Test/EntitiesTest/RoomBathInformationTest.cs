using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class RoomBathInformationTest
    {
        [Fact]
        public void RoomBathInformation_CanSet_RoomTemplateID()
        {
            // Arrange
            var roomBathInfo = new RoomBathInformation();
            var roomTemplateId = Guid.NewGuid();

            // Act
            roomBathInfo.RoomTemplateID = roomTemplateId;

            // Assert
            Assert.Equal(roomTemplateId, roomBathInfo.RoomTemplateID);
        }

        [Fact]
        public void RoomBathInformation_CanGet_RoomTemplateID()
        {
            // Arrange
            var roomTemplateId = Guid.NewGuid();
            var roomBathInfo = new RoomBathInformation { RoomTemplateID = roomTemplateId };

            // Act & Assert
            Assert.Equal(roomTemplateId, roomBathInfo.RoomTemplateID);
        }

        [Fact]
        public void RoomBathInformation_CanSet_BathRoomID()
        {
            // Arrange
            var roomBathInfo = new RoomBathInformation();
            var bathRoomId = Guid.NewGuid();

            // Act
            roomBathInfo.BathRoomID = bathRoomId;

            // Assert
            Assert.Equal(bathRoomId, roomBathInfo.BathRoomID);
        }

        [Fact]
        public void RoomBathInformation_CanGet_BathRoomID()
        {
            // Arrange
            var bathRoomId = Guid.NewGuid();
            var roomBathInfo = new RoomBathInformation { BathRoomID = bathRoomId };

            // Act & Assert
            Assert.Equal(bathRoomId, roomBathInfo.BathRoomID);
        }

        [Fact]
        public void RoomBathInformation_CanSet_Quantity()
        {
            // Arrange
            var roomBathInfo = new RoomBathInformation();
            var quantity = 2;

            // Act
            roomBathInfo.Quantity = quantity;

            // Assert
            Assert.Equal(quantity, roomBathInfo.Quantity);
        }

        [Fact]
        public void RoomBathInformation_CanGet_Quantity()
        {
            // Arrange
            var quantity = 2;
            var roomBathInfo = new RoomBathInformation { Quantity = quantity };

            // Act & Assert
            Assert.Equal(quantity, roomBathInfo.Quantity);
        }

        [Fact]
        public void RoomBathInformation_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var roomBathInfo = new RoomBathInformation();

            // Assert
            Assert.Equal(Guid.Empty, roomBathInfo.RoomTemplateID);
            Assert.Equal(Guid.Empty, roomBathInfo.BathRoomID);
            Assert.Equal(0, roomBathInfo.Quantity);
        }
    }
}
