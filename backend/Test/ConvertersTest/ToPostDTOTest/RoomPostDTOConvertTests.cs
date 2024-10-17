using Xunit;
using DTOs.WithId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class RoomPostDTOConvertTests
    {
        private readonly RoomPostDTOConvert _converter;

        public RoomPostDTOConvertTests()
        {
            _converter = new RoomPostDTOConvert();
        }

        [Fact]
        public void Convert_ValidRoomAndRelatedEntities_ReturnsRoomPostDTO()
        {
            // Arrange
            var room = new Room
            {
                Code = "R101",
                FloorNumber = 1,
                PricePerNight = 150.00m,
                RoomTemplateID = Guid.NewGuid(),
                RoomID = Guid.NewGuid()
            };

            var roomTemplate = new RoomTemplate
            {
                RoomTemplateID = room.RoomTemplateID,
                Side = "Ocean View",
                Windows = 2
            };

            var hotel = new Hotel
            {
                HotelID = Guid.NewGuid(),
                Name = "Seaside Hotel",
                AllowsPets = true
            };

            var bedInformations = new List<BedInformation>
            {
                new BedInformation { RoomTemplateID = roomTemplate.RoomTemplateID, BedID = Guid.NewGuid(), Quantity = 1 }
            };

            var roomBathInformations = new List<RoomBathInformation>
            {
                new RoomBathInformation { RoomTemplateID = roomTemplate.RoomTemplateID, BathRoomID = Guid.NewGuid(), Quantity = 1 }
            };

            var services = new List<RoomServices>
            {
                new RoomServices { RoomID = room.RoomID, ServiceID = Guid.NewGuid() }
            };

            // Act
            var result = _converter.Convert(room, roomTemplate, hotel, bedInformations, roomBathInformations, services);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(room.Code, result.Code);
            Assert.Equal(room.FloorNumber, result.FloorNumber);
            Assert.Equal(room.PricePerNight, result.PricePerNight);
            Assert.Equal(roomTemplate.Side, result.RoomTemplateSide);
            Assert.Equal(roomTemplate.Windows, result.RoomTemplateWindows);
            Assert.Equal(hotel.Name, result.HotelName);
            Assert.Equal(hotel.AllowsPets, result.HotelAllowsPets);
            Assert.Equal(1, result.Beds.Count);
            Assert.Equal(bedInformations[0].BedID, result.Beds[0]);
            Assert.Equal(1, result.Bathrooms.Count);
            Assert.Equal(roomBathInformations[0].BathRoomID, result.Bathrooms[0]);
            Assert.Equal(1, result.Services.Count);
            Assert.Equal(services[0].ServiceID, result.Services[0]);
        }
    }
}
