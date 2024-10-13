using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class RoomConverter : IConverter1To6<Room, RoomTemplate, Hotel, List<BedInformation>, List<RoomBathInformation>,List<RoomServices>,  RoomDTO>, IConverter1To2<RoomPostDTO, Guid, RoomDTO>
    {
        public RoomDTO Convert(Room room, RoomTemplate roomTemplate, Hotel hotel, List<BedInformation> bedInformations, List<RoomBathInformation> roomBathInformations, List<RoomServices> services)
        {
            var bedList = bedInformations
                .Where(b => b.RoomTemplateID == room.RoomTemplateID)
                .Select(b => b.BedID)
                .ToList();

            var bathList = roomBathInformations
                .Where(b => b.RoomTemplateID == room.RoomTemplateID)
                .Select(b => b.BathRoomID)
                .ToList();
            
            var serviceList = services
                .Where(s => s.RoomID == room.RoomID)
                .Select(s => s.ServiceID)
                .ToList();

            return new RoomDTO
            {
                RoomID = room.RoomID,
                Code = room.Code,
                FloorNumber = room.FloorNumber,
                PricePerNight = room.PricePerNight,
                RoomTemplateSide = roomTemplate.Side,
                RoomTemplateWindows = roomTemplate.Windows,
                HotelName = hotel.Name,
                HotelAllowsPets = hotel.AllowsPets,
                Beds = bedList,
                Bathrooms = bathList,
                Services = serviceList
            };
        }

        public RoomDTO Convert(RoomPostDTO roomPostDto, Guid id)
        {
            return new RoomDTO()
            {
                Bathrooms = roomPostDto.Bathrooms,
                Beds = roomPostDto.Beds,
                Code = roomPostDto.Code,
                FloorNumber = roomPostDto.FloorNumber,
                HotelName = roomPostDto.HotelName,
                HotelAllowsPets = roomPostDto.HotelAllowsPets,
                PricePerNight = roomPostDto.PricePerNight,
                RoomTemplateSide = roomPostDto.RoomTemplateSide,
                RoomTemplateWindows = roomPostDto.RoomTemplateWindows,
                RoomID = id,
                Services = roomPostDto.Services
            };
        }
    }
}
