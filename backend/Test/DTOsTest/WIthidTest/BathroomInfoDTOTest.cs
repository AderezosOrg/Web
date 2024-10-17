using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class BathroomInfoDTOTest
    {
        [Fact]
        public void BathroomInfoDTO_CanSet_BathRoomID()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO();
            var bathroomId = Guid.NewGuid();

            // Act
            bathroomInfoDTO.BathRoomID = bathroomId;

            // Assert
            Assert.Equal(bathroomId, bathroomInfoDTO.BathRoomID);
        }

        [Fact]
        public void BathroomInfoDTO_CanGet_BathRoomID()
        {
            // Arrange
            var bathroomId = Guid.NewGuid();
            var bathroomInfoDTO = new BathroomInfoDTO { BathRoomID = bathroomId };

            // Act & Assert
            Assert.Equal(bathroomId, bathroomInfoDTO.BathRoomID);
        }

        [Fact]
        public void BathroomInfoDTO_CanSet_Shower()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO();

            // Act
            bathroomInfoDTO.Shower = true;

            // Assert
            Assert.True(bathroomInfoDTO.Shower);
        }

        [Fact]
        public void BathroomInfoDTO_CanGet_Shower()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO { Shower = true };

            // Act & Assert
            Assert.True(bathroomInfoDTO.Shower);
        }

        [Fact]
        public void BathroomInfoDTO_CanSet_Toilet()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO();

            // Act
            bathroomInfoDTO.Toilet = true;

            // Assert
            Assert.True(bathroomInfoDTO.Toilet);
        }

        [Fact]
        public void BathroomInfoDTO_CanGet_Toilet()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO { Toilet = true };

            // Act & Assert
            Assert.True(bathroomInfoDTO.Toilet);
        }

        [Fact]
        public void BathroomInfoDTO_CanSet_DressingTable()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO();

            // Act
            bathroomInfoDTO.DressingTable = true;

            // Assert
            Assert.True(bathroomInfoDTO.DressingTable);
        }

        [Fact]
        public void BathroomInfoDTO_CanGet_DressingTable()
        {
            // Arrange
            var bathroomInfoDTO = new BathroomInfoDTO { DressingTable = true };

            // Act & Assert
            Assert.True(bathroomInfoDTO.DressingTable);
        }

        [Fact]
        public void BathroomInfoDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var bathroomInfoDTO = new BathroomInfoDTO();

            // Assert
            Assert.Equal(Guid.Empty, bathroomInfoDTO.BathRoomID);
            Assert.False(bathroomInfoDTO.Shower);
            Assert.False(bathroomInfoDTO.Toilet);
            Assert.False(bathroomInfoDTO.DressingTable);
        }
    }
}
