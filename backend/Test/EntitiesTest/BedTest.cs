using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class BedTest
    {
        [Fact]
        public void Bed_CanSet_BedID()
        {
            // Arrange
            var bed = new Bed();
            var bedId = Guid.NewGuid();

            // Act
            bed.BedID = bedId;

            // Assert
            Assert.Equal(bedId, bed.BedID);
        }

        [Fact]
        public void Bed_CanGet_BedID()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            var bed = new Bed { BedID = bedId };

            // Act & Assert
            Assert.Equal(bedId, bed.BedID);
        }

        [Fact]
        public void Bed_CanSet_Size()
        {
            // Arrange
            var bed = new Bed();
            var size = "King";

            // Act
            bed.Size = size;

            // Assert
            Assert.Equal(size, bed.Size);
        }

        [Fact]
        public void Bed_CanGet_Size()
        {
            // Arrange
            var size = "Queen";
            var bed = new Bed { Size = size };

            // Act & Assert
            Assert.Equal(size, bed.Size);
        }

        [Fact]
        public void Bed_CanSet_Capacity()
        {
            // Arrange
            var bed = new Bed();
            var capacity = 1;

            // Act
            bed.Capacity = capacity;

            // Assert
            Assert.Equal(capacity, bed.Capacity);
        }

        [Fact]
        public void Bed_CanGet_Capacity()
        {
            // Arrange
            var capacity = 1;
            var bed = new Bed { Capacity = capacity };

            // Act & Assert
            Assert.Equal(capacity, bed.Capacity);
        }

        [Fact]
        public void Bed_DefaultValues_AreNullOrEmpty()
        {
            // Arrange & Act
            var bed = new Bed();

            // Assert
            Assert.Null(bed.Size);
            Assert.Equal(bed.Capacity, 0);
        }
    }
}
