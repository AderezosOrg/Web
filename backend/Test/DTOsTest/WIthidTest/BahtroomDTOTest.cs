using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class BathroomDTOTest
    {
        [Fact]
        public void BathroomDTO_CanSet_BathRoomID()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO();
            var bathroomId = Guid.NewGuid();

            // Act
            bathroomDTO.BathRoomID = bathroomId;

            // Assert
            Assert.Equal(bathroomId, bathroomDTO.BathRoomID);
        }

        [Fact]
        public void BathroomDTO_CanGet_BathRoomID()
        {
            // Arrange
            var bathroomId = Guid.NewGuid();
            var bathroomDTO = new BathroomDTO { BathRoomID = bathroomId };

            // Act & Assert
            Assert.Equal(bathroomId, bathroomDTO.BathRoomID);
        }

        [Fact]
        public void BathroomDTO_CanSet_Shower()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO();

            // Act
            bathroomDTO.Shower = true;

            // Assert
            Assert.True(bathroomDTO.Shower);
        }

        [Fact]
        public void BathroomDTO_CanGet_Shower()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO { Shower = true };

            // Act & Assert
            Assert.True(bathroomDTO.Shower);
        }

        [Fact]
        public void BathroomDTO_CanSet_Toilet()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO();

            // Act
            bathroomDTO.Toilet = true;

            // Assert
            Assert.True(bathroomDTO.Toilet);
        }

        [Fact]
        public void BathroomDTO_CanGet_Toilet()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO { Toilet = true };

            // Act & Assert
            Assert.True(bathroomDTO.Toilet);
        }

        [Fact]
        public void BathroomDTO_CanSet_DressingTable()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO();

            // Act
            bathroomDTO.DressingTable = true;

            // Assert
            Assert.True(bathroomDTO.DressingTable);
        }

        [Fact]
        public void BathroomDTO_CanGet_DressingTable()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO { DressingTable = true };

            // Act & Assert
            Assert.True(bathroomDTO.DressingTable);
        }

        [Fact]
        public void BathroomDTO_CanSet_BathroomQuantity()
        {
            // Arrange
            var bathroomDTO = new BathroomDTO();
            var quantity = 2;

            // Act
            bathroomDTO.BathroomQuantity = quantity;

            // Assert
            Assert.Equal(quantity, bathroomDTO.BathroomQuantity);
        }

        [Fact]
        public void BathroomDTO_CanGet_BathroomQuantity()
        {
            // Arrange
            var quantity = 2;
            var bathroomDTO = new BathroomDTO { BathroomQuantity = quantity };

            // Act & Assert
            Assert.Equal(quantity, bathroomDTO.BathroomQuantity);
        }

        [Fact]
        public void BathroomDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var bathroomDTO = new BathroomDTO();

            // Assert
            Assert.Equal(Guid.Empty, bathroomDTO.BathRoomID);
            Assert.False(bathroomDTO.Shower);
            Assert.False(bathroomDTO.Toilet);
            Assert.False(bathroomDTO.DressingTable);
            Assert.Equal(0, bathroomDTO.BathroomQuantity);
        }
    }
}
