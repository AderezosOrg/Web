using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class BathroomPostDTOTest
    {
        [Fact]
        public void BathroomPostDTO_CanSet_Shower()
        {
            // Arrange
            var bathroomPostDTO = new BathroomPostDTO();
            var shower = true;

            // Act
            bathroomPostDTO.Shower = shower;

            // Assert
            Assert.True(bathroomPostDTO.Shower);
        }

        [Fact]
        public void BathroomPostDTO_CanGet_Shower()
        {
            // Arrange
            var bathroomPostDTO = new BathroomPostDTO { Shower = true };

            // Act & Assert
            Assert.True(bathroomPostDTO.Shower);
        }

        [Fact]
        public void BathroomPostDTO_CanSet_Toilet()
        {
            // Arrange
            var bathroomPostDTO = new BathroomPostDTO();
            var toilet = false;

            // Act
            bathroomPostDTO.Toilet = toilet;

            // Assert
            Assert.False(bathroomPostDTO.Toilet);
        }

        [Fact]
        public void BathroomPostDTO_CanGet_Toilet()
        {
            // Arrange
            var bathroomPostDTO = new BathroomPostDTO { Toilet = false };

            // Act & Assert
            Assert.False(bathroomPostDTO.Toilet);
        }

        [Fact]
        public void BathroomPostDTO_CanSet_DressingTable()
        {
            // Arrange
            var bathroomPostDTO = new BathroomPostDTO();
            var dressingTable = true;

            // Act
            bathroomPostDTO.DressingTable = dressingTable;

            // Assert
            Assert.True(bathroomPostDTO.DressingTable);
        }

        [Fact]
        public void BathroomPostDTO_CanGet_DressingTable()
        {
            // Arrange
            var bathroomPostDTO = new BathroomPostDTO { DressingTable = true };

            // Act & Assert
            Assert.True(bathroomPostDTO.DressingTable);
        }

        [Fact]
        public void BathroomPostDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var bathroomPostDTO = new BathroomPostDTO();

            // Assert
            Assert.False(bathroomPostDTO.Shower);
            Assert.False(bathroomPostDTO.Toilet);
            Assert.False(bathroomPostDTO.DressingTable);
        }
    }
}
