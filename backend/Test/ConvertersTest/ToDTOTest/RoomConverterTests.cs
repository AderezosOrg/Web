using Xunit;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class RoomConverterTests
    {
        private readonly RoomConverter _converter;

        public RoomConverterTests()
        {
            _converter = new RoomConverter();
        }

        [Fact]
        public void Convert_RoomEntities_ReturnsRoomDTO()
        {
            // Arrange
            var room = new Room
            {
                RoomID = Guid.NewGuid(),
                Code = "101",
                FloorNumber = 1,
                PricePerNight = 100m,
                RoomTemplateID = Guid.NewGuid()
            };

            var roomTemplate = new RoomTemplate
            {
                RoomTemplateID = room.RoomTemplateID,
                Side = "North",
                Windows = 2
            };

            var hotel = new Hotel
            {
                HotelID = Guid.NewGuid(),
                Name = "Hotel Test",
                AllowsPets = true
            };

            var bedInformations = new List<BedInformation>
            {
                new BedInformation { RoomTemplateID = room.RoomTemplateID, BedID = Guid.NewGuid(), Quantity = 2 }
            };

            var roomBathInformations = new List<RoomBathInformation>
            {
                new RoomBathInformation { RoomTemplateID = room.RoomTemplateID, BathRoomID = Guid.NewGuid(), Quantity = 1 }
            };

            var services = new List<RoomServices>
            {
                new RoomServices { RoomID = room.RoomID, ServiceID = Guid.NewGuid() }
            };

            // Act
            var result = _converter.Convert(room, roomTemplate, hotel, bedInformations, roomBathInformations, services);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(room.RoomID, result.RoomID);
            Assert.Equal(room.Code, result.Code);
            Assert.Equal(room.FloorNumber, result.FloorNumber);
            Assert.Equal(room.PricePerNight, result.PricePerNight);
            Assert.Equal(roomTemplate.Side, result.RoomTemplateSide);
            Assert.Equal(roomTemplate.Windows, result.RoomTemplateWindows);
            Assert.Equal(hotel.Name, result.HotelName);
            Assert.Equal(hotel.AllowsPets, result.HotelAllowsPets);
            Assert.Single(result.Beds);
            Assert.Single(result.Bathrooms);
            Assert.Single(result.Services);
        }

        [Fact]
        public void Convert_RoomPostDTO_ReturnsRoomDTO()
        {
            // Arrange
            var roomPostDto = new RoomPostDTO
            {
                Code = "102",
                FloorNumber = 2,
                PricePerNight = 150m,
                RoomTemplateSide = "South",
                RoomTemplateWindows = 1,
                HotelName = "Hotel Example",
                HotelAllowsPets = false,
                Beds = new List<Guid> { Guid.NewGuid() },
                Bathrooms = new List<Guid> { Guid.NewGuid() },
                Services = new List<Guid> { Guid.NewGuid() }
            };

            var roomId = Guid.NewGuid();

            // Act
            var result = _converter.Convert(roomPostDto, roomId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(roomId, result.RoomID);
            Assert.Equal(roomPostDto.Code, result.Code);
            Assert.Equal(roomPostDto.FloorNumber, result.FloorNumber);
            Assert.Equal(roomPostDto.PricePerNight, result.PricePerNight);
            Assert.Equal(roomPostDto.RoomTemplateSide, result.RoomTemplateSide);
            Assert.Equal(roomPostDto.RoomTemplateWindows, result.RoomTemplateWindows);
            Assert.Equal(roomPostDto.HotelName, result.HotelName);
            Assert.Equal(roomPostDto.HotelAllowsPets, result.HotelAllowsPets);
            Assert.Equal(roomPostDto.Beds, result.Beds);
            Assert.Equal(roomPostDto.Bathrooms, result.Bathrooms);
            Assert.Equal(roomPostDto.Services, result.Services);
        }

        [Fact]
        public void Convert_RoomDTOAndPostDTOLists_ReturnsRoomFullInfoDTO()
        {
            // Arrange
            var roomDto = new RoomDTO
            {
                RoomID = Guid.NewGuid(),
                Code = "103",
                FloorNumber = 3,
                PricePerNight = 200m,
                RoomTemplateSide = "East",
                RoomTemplateWindows = 3,
                HotelName = "Another Hotel",
                HotelAllowsPets = true
            };

            var bathrooms = new List<BathroomPostDTO>
            {
                new BathroomPostDTO { DressingTable = true, Shower = true, Toilet = true }
            };

            var beds = new List<BedPostDTO>
            {
                new BedPostDTO { Size = "Queen", Capacity = 2 }
            };

            var services = new List<ServicePostDTO>
            {
                new ServicePostDTO { Type = "Breakfast" }
            };

            // Act
            var result = _converter.Convert(roomDto, bathrooms, beds, services);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(roomDto.RoomID, result.RoomID);
            Assert.Equal(roomDto.Code, result.Code);
            Assert.Equal(roomDto.FloorNumber, result.FloorNumber);
            Assert.Equal(roomDto.PricePerNight, result.PricePerNight);
            Assert.Equal(roomDto.RoomTemplateSide, result.RoomTemplateSide);
            Assert.Equal(roomDto.RoomTemplateWindows, result.RoomTemplateWindows);
            Assert.Equal(roomDto.HotelName, result.HotelName);
            Assert.Equal(roomDto.HotelAllowsPets, result.HotelAllowsPets);
            Assert.Equal(bathrooms, result.Bathrooms);
            Assert.Equal(beds, result.Beds);
            Assert.Equal(services, result.Services);
        }
    }
}
