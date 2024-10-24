using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class BedPostDTOTest
    {
        [Fact]
        public void BedPostDTO_CanSet_Size()
        {
            // Arrange
            var bedPostDTO = new BedPostDTO();
            var size = "Queen";

            // Act
            bedPostDTO.Size = size;

            // Assert
            Assert.Equal(size, bedPostDTO.Size);
        }

        [Fact]
        public void BedPostDTO_CanGet_Size()
        {
            // Arrange
            var bedPostDTO = new BedPostDTO { Size = "King" };

            // Act & Assert
            Assert.Equal("King", bedPostDTO.Size);
        }

        [Fact]
        public void BedPostDTO_CanSet_Capacity()
        {
            // Arrange
            var bedPostDTO = new BedPostDTO();
            var capacity = 2;

            // Act
            bedPostDTO.Capacity = capacity;

            // Assert
            Assert.Equal(capacity, bedPostDTO.Capacity);
        }

        [Fact]
        public void BedPostDTO_CanGet_Capacity()
        {
            // Arrange
            var bedPostDTO = new BedPostDTO { Capacity = 1 };

            // Act & Assert
            Assert.Equal(1, bedPostDTO.Capacity);
        }

        [Fact]
        public void BedPostDTO_DefaultValues_AreNull()
        {
            // Arrange & Act
            var bedPostDTO = new BedPostDTO();

            // Assert
            Assert.Null(bedPostDTO.Size);
            Assert.Equal(bedPostDTO.Capacity, 0);
        }
    }
}