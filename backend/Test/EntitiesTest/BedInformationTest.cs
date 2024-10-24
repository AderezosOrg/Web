using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class BedInformationTest
    {
        [Fact]
        public void BedInformation_CanSet_RoomTemplateID()
        {
            // Arrange
            var bedInformation = new BedInformation();
            var roomTemplateId = Guid.NewGuid();

            // Act
            bedInformation.RoomTemplateID = roomTemplateId;

            // Assert
            Assert.Equal(roomTemplateId, bedInformation.RoomTemplateID);
        }

        [Fact]
        public void BedInformation_CanGet_RoomTemplateID()
        {
            // Arrange
            var roomTemplateId = Guid.NewGuid();
            var bedInformation = new BedInformation { RoomTemplateID = roomTemplateId };

            // Act & Assert
            Assert.Equal(roomTemplateId, bedInformation.RoomTemplateID);
        }

        [Fact]
        public void BedInformation_CanSet_BedID()
        {
            // Arrange
            var bedInformation = new BedInformation();
            var bedId = Guid.NewGuid();

            // Act
            bedInformation.BedID = bedId;

            // Assert
            Assert.Equal(bedId, bedInformation.BedID);
        }

        [Fact]
        public void BedInformation_CanGet_BedID()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            var bedInformation = new BedInformation { BedID = bedId };

            // Act & Assert
            Assert.Equal(bedId, bedInformation.BedID);
        }

        [Fact]
        public void BedInformation_CanSet_Quantity()
        {
            // Arrange
            var bedInformation = new BedInformation();
            var quantity = 5;

            // Act
            bedInformation.Quantity = quantity;

            // Assert
            Assert.Equal(quantity, bedInformation.Quantity);
        }

        [Fact]
        public void BedInformation_CanGet_Quantity()
        {
            // Arrange
            var quantity = 3;
            var bedInformation = new BedInformation { Quantity = quantity };

            // Act & Assert
            Assert.Equal(quantity, bedInformation.Quantity);
        }

        [Fact]
        public void BedInformation_DefaultQuantity_IsZero()
        {
            // Arrange & Act
            var bedInformation = new BedInformation();

            // Assert
            Assert.Equal(0, bedInformation.Quantity);
        }
    }
}
