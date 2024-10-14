using Xunit;
using DTOs.WithoutId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class BathroomPostConverterTests
    {
        private readonly BathroomPostConverter _converter;

        public BathroomPostConverterTests()
        {
            _converter = new BathroomPostConverter();
        }

        [Fact]
        public void Convert_Bathroom_ReturnsBathroomPostDTO()
        {
            // Arrange
            var bathroom = new Bathroom
            {
                Shower = true,
                Toilet = true,
                DressingTable = true
            };

            // Act
            var result = _converter.Convert(bathroom);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bathroom.Shower, result.Shower);
            Assert.Equal(bathroom.Toilet, result.Toilet);
            Assert.Equal(bathroom.DressingTable, result.DressingTable);
        }

        [Fact]
        public void Convert_BathroomWithDefaults_ReturnsBathroomPostDTOWithDefaults()
        {
            // Arrange
            var bathroom = new Bathroom
            {
                Shower = false,
                Toilet = false,
                DressingTable = false
            };

            // Act
            var result = _converter.Convert(bathroom);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bathroom.Shower, result.Shower);
            Assert.Equal(bathroom.Toilet, result.Toilet);
            Assert.Equal(bathroom.DressingTable, result.DressingTable);
        }
    }
}