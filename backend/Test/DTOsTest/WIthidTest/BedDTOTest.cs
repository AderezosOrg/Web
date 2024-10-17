using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class BedDTOTest
    {
        [Fact]
        public void BedDTO_CanSet_BedID()
        {
            // Arrange
            var bedDTO = new BedDTO();
            var bedId = Guid.NewGuid();

            // Act
            bedDTO.BedID = bedId;

            // Assert
            Assert.Equal(bedId, bedDTO.BedID);
        }

        [Fact]
        public void BedDTO_CanGet_BedID()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            var bedDTO = new BedDTO { BedID = bedId };

            // Act & Assert
            Assert.Equal(bedId, bedDTO.BedID);
        }

        [Fact]
        public void BedDTO_CanSet_Size()
        {
            // Arrange
            var bedDTO = new BedDTO();
            var size = "Queen";

            // Act
            bedDTO.Size = size;

            // Assert
            Assert.Equal(size, bedDTO.Size);
        }

        [Fact]
        public void BedDTO_CanGet_Size()
        {
            // Arrange
            var size = "King";
            var bedDTO = new BedDTO { Size = size };

            // Act & Assert
            Assert.Equal(size, bedDTO.Size);
        }

        [Fact]
        public void BedDTO_CanSet_Capacity()
        {
            // Arrange
            var bedDTO = new BedDTO();
            var capacity = "2 people";

            // Act
            bedDTO.Capacity = capacity;

            // Assert
            Assert.Equal(capacity, bedDTO.Capacity);
        }

        [Fact]
        public void BedDTO_CanGet_Capacity()
        {
            // Arrange
            var capacity = "1 person";
            var bedDTO = new BedDTO { Capacity = capacity };

            // Act & Assert
            Assert.Equal(capacity, bedDTO.Capacity);
        }

        [Fact]
        public void BedDTO_CanSet_BedQuantity()
        {
            // Arrange
            var bedDTO = new BedDTO();
            var quantity = 3;

            // Act
            bedDTO.BedQuantity = quantity;

            // Assert
            Assert.Equal(quantity, bedDTO.BedQuantity);
        }

        [Fact]
        public void BedDTO_CanGet_BedQuantity()
        {
            // Arrange
            var quantity = 5;
            var bedDTO = new BedDTO { BedQuantity = quantity };

            // Act & Assert
            Assert.Equal(quantity, bedDTO.BedQuantity);
        }

        [Fact]
        public void BedDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var bedDTO = new BedDTO();

            // Assert
            Assert.Equal(Guid.Empty, bedDTO.BedID);
            Assert.Null(bedDTO.Size);
            Assert.Null(bedDTO.Capacity);
            Assert.Equal(0, bedDTO.BedQuantity);
        }
    }
}
