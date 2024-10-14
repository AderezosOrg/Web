using Xunit;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class BathroomConverterTests
    {
        private readonly BathroomConverter _converter;

        public BathroomConverterTests()
        {
            _converter = new BathroomConverter();
        }

        [Fact]
        public void Convert_BathroomAndRoomBathInformation_ReturnsBathroomDTO()
        {
            // Arrange
            var bathroom = new Bathroom
            {
                BathRoomID = Guid.NewGuid(),
                Shower = true,
                Toilet = true,
                DressingTable = false
            };

            var roomBathInformation = new RoomBathInformation
            {
                BathRoomID = bathroom.BathRoomID,
                Quantity = 2
            };

            // Act
            var result = _converter.Convert(bathroom, roomBathInformation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bathroom.BathRoomID, result.BathRoomID);
            Assert.Equal(bathroom.Shower, result.Shower);
            Assert.Equal(bathroom.Toilet, result.Toilet);
            Assert.Equal(bathroom.DressingTable, result.DressingTable);
            Assert.Equal(roomBathInformation.Quantity, result.BathroomQuantity);
        }

        [Fact]
        public void Convert_BathroomPostDTOAndId_ReturnsBathroomDTO()
        {
            // Arrange
            var postDto = new BathroomPostDTO
            {
                Shower = true,
                Toilet = true,
                DressingTable = false
            };
            var id = Guid.NewGuid();

            // Act
            var result = _converter.Convert(postDto, id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.BathRoomID);
            Assert.Equal(postDto.Shower, result.Shower);
            Assert.Equal(postDto.Toilet, result.Toilet);
            Assert.Equal(postDto.DressingTable, result.DressingTable);
        }

        [Fact]
        public void Convert_Bathroom_ReturnsBathroomInfoDTO()
        {
            // Arrange
            var bathroom = new Bathroom
            {
                BathRoomID = Guid.NewGuid(),
                Shower = true,
                Toilet = true,
                DressingTable = false
            };

            // Act
            var result = _converter.Convert(bathroom);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bathroom.BathRoomID, result.BathRoomID);
            Assert.Equal(bathroom.Shower, result.Shower);
            Assert.Equal(bathroom.Toilet, result.Toilet);
            Assert.Equal(bathroom.DressingTable, result.DressingTable);
        }
    }
}
