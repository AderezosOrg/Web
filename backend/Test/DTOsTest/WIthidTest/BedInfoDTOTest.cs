using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class BedInfoDTOTest
    {
        [Fact]
        public void BedInfoDTO_CanSet_BedID()
        {
            // Arrange
            var bedInfoDTO = new BedInfoDTO();
            var bedId = Guid.NewGuid();

            // Act
            bedInfoDTO.BedID = bedId;

            // Assert
            Assert.Equal(bedId, bedInfoDTO.BedID);
        }

        [Fact]
        public void BedInfoDTO_CanGet_BedID()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            var bedInfoDTO = new BedInfoDTO { BedID = bedId };

            // Act & Assert
            Assert.Equal(bedId, bedInfoDTO.BedID);
        }

        [Fact]
        public void BedInfoDTO_CanSet_Size()
        {
            // Arrange
            var bedInfoDTO = new BedInfoDTO();
            var size = "Full";

            // Act
            bedInfoDTO.Size = size;

            // Assert
            Assert.Equal(size, bedInfoDTO.Size);
        }

        [Fact]
        public void BedInfoDTO_CanGet_Size()
        {
            // Arrange
            var size = "Twin";
            var bedInfoDTO = new BedInfoDTO { Size = size };

            // Act & Assert
            Assert.Equal(size, bedInfoDTO.Size);
        }

        [Fact]
        public void BedInfoDTO_CanSet_Capacity()
        {
            // Arrange
            var bedInfoDTO = new BedInfoDTO();
            var capacity = 1;

            // Act
            bedInfoDTO.Capacity = capacity;

            // Assert
            Assert.Equal(capacity, bedInfoDTO.Capacity);
        }

        [Fact]
        public void BedInfoDTO_CanGet_Capacity()
        {
            // Arrange
            var capacity = 2;
            var bedInfoDTO = new BedInfoDTO { Capacity = capacity };

            // Act & Assert
            Assert.Equal(capacity, bedInfoDTO.Capacity);
        }

        [Fact]
        public void BedInfoDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var bedInfoDTO = new BedInfoDTO();

            // Assert
            Assert.Equal(Guid.Empty, bedInfoDTO.BedID);
            Assert.Null(bedInfoDTO.Size);
            Assert.Null(bedInfoDTO.Capacity);
        }
    }
}
