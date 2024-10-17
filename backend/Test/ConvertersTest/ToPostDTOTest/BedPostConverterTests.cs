using Xunit;
using DTOs.WithoutId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class BedPostConverterTests
    {
        private readonly BedPostConverter _converter;

        public BedPostConverterTests()
        {
            _converter = new BedPostConverter();
        }

        [Fact]
        public void Convert_Bed_ReturnsBedPostDTO()
        {
            // Arrange
            var bed = new Bed
            {
                Size = "Queen",
                Capacity = "2"
            };

            // Act
            var result = _converter.Convert(bed);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bed.Size, result.Size);
            Assert.Equal(bed.Capacity, result.Capacity);
        }

        [Fact]
        public void Convert_BedWithDefaults_ReturnsBedPostDTOWithDefaults()
        {
            // Arrange
            var bed = new Bed
            {
                Size = "Single",
                Capacity = "1"
            };

            // Act
            var result = _converter.Convert(bed);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bed.Size, result.Size);
            Assert.Equal(bed.Capacity, result.Capacity);
        }
    }
}