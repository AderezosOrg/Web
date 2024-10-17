using Xunit;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class BedConverterTests
    {
        private readonly BedConverter _converter;

        public BedConverterTests()
        {
            _converter = new BedConverter();
        }

        [Fact]
        public void Convert_BedAndBedInformation_ReturnsBedDTO()
        {
            // Arrange
            var bed = new Bed
            {
                BedID = Guid.NewGuid(),
                Size = "King",
                Capacity = "2"
            };

            var bedInformation = new BedInformation
            {
                BedID = bed.BedID,
                Quantity = 1
            };

            // Act
            var result = _converter.Convert(bed, bedInformation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bed.BedID, result.BedID);
            Assert.Equal(bed.Size, result.Size);
            Assert.Equal(bed.Capacity, result.Capacity);
            Assert.Equal(bedInformation.Quantity, result.BedQuantity);
        }

        [Fact]
        public void Convert_BedPostDTOAndId_ReturnsBedDTO()
        {
            // Arrange
            var postDto = new BedPostDTO
            {
                Size = "Queen",
                Capacity = "2"
            };
            var id = Guid.NewGuid();

            // Act
            var result = _converter.Convert(postDto, id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.BedID);
            Assert.Equal(postDto.Size, result.Size);
            Assert.Equal(postDto.Capacity, result.Capacity);
        }

        [Fact]
        public void Convert_Bed_ReturnsBedInfoDTO()
        {
            // Arrange
            var bed = new Bed
            {
                BedID = Guid.NewGuid(),
                Size = "Twin",
                Capacity = "1"
            };

            // Act
            var result = _converter.Convert(bed);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bed.BedID, result.BedID);
            Assert.Equal(bed.Size, result.Size);
            Assert.Equal(bed.Capacity, result.Capacity);
        }
    }
}
