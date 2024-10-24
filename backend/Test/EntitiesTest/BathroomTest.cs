using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class BathroomTest
    {
        [Fact]
        public void Bathroom_CanSet_BathRoomID()
        {
            // Arrange
            var bathroom = new Bathroom();
            var bathroomId = Guid.NewGuid();

            // Act
            bathroom.BathRoomID = bathroomId;

            // Assert
            Assert.Equal(bathroomId, bathroom.BathRoomID); 
        }

        [Fact]
        public void Bathroom_CanGet_BathRoomID()
        {
            // Arrange
            var bathroomId = Guid.NewGuid();
            var bathroom = new Bathroom { BathRoomID = bathroomId };

            // Act & Assert
            Assert.Equal(bathroomId, bathroom.BathRoomID); 
        }

        [Fact]
        public void Bathroom_CanSet_Shower()
        {
            // Arrange
            var bathroom = new Bathroom();

            // Act
            bathroom.Shower = true;

            // Assert
            Assert.True(bathroom.Shower);
        }

        [Fact]
        public void Bathroom_CanGet_Shower()
        {
            // Arrange
            var bathroom = new Bathroom { Shower = true };

            // Act & Assert
            Assert.True(bathroom.Shower); 
        }

        [Fact]
        public void Bathroom_CanSet_Toilet()
        {
            // Arrange
            var bathroom = new Bathroom();

            // Act
            bathroom.Toilet = true;

            // Assert
            Assert.True(bathroom.Toilet); 
        }

        [Fact]
        public void Bathroom_CanGet_Toilet()
        {
            // Arrange
            var bathroom = new Bathroom { Toilet = true };

            // Act & Assert
            Assert.True(bathroom.Toilet); 
        }

        [Fact]
        public void Bathroom_CanSet_DressingTable()
        {
            // Arrange
            var bathroom = new Bathroom();

            // Act
            bathroom.DressingTable = true;

            // Assert
            Assert.True(bathroom.DressingTable); 
        }

        [Fact]
        public void Bathroom_CanGet_DressingTable()
        {
            // Arrange
            var bathroom = new Bathroom { DressingTable = true };

            // Act & Assert
            Assert.True(bathroom.DressingTable); 
        }
        
        [Fact]
        public void Bathroom_DefaultValues_AreFalse()
        {
            // Arrange & Act
            var bathroom = new Bathroom();

            // Assert
            Assert.False(bathroom.Shower);
            Assert.False(bathroom.Toilet);
            Assert.False(bathroom.DressingTable);
        }
    }
}
