using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class RoomPostDTOConvert : IConverter1To6<Room, RoomTemplate, Hotel, List<BedInformation>, List<RoomBathInformation>,List<RoomServices>,  RoomPostDTO>
{
    public RoomPostDTO Convert(Room room, RoomTemplate roomTemplate, Hotel hotel, List<BedInformation> bedInformations, List<RoomBathInformation> roomBathInformations, List<RoomServices> services)
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

        return new RoomPostDTO
        {
            Code = room.Code,
            FloorNumber = room.FloorNumber,
            PricePerNight = room.PricePerNight,
            RoomTemplateSide = roomTemplate.Side,
            RoomTemplateWindows = roomTemplate.Windows,
            HotelName = hotel.Name,
            HotelAllowsPets = hotel.AllowsPets,
            Tax = hotel.Tax,
            Beds = bedList,
            Bathrooms = bathList,
            Services = serviceList
        };
    }
}
