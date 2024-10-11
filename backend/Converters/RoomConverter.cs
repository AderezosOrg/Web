using DTOs;
using Entities;
using IConverters;

namespace Converters
{
    public class RoomConverter : IConverter1To3<Room, RoomTemplate, Hotel, RoomDTO>
    {
        public RoomDTO Convert(Room room, RoomTemplate roomTemplate, Hotel hotel)
        {
            return new RoomDTO
            {
                RoomID = room.RoomID,
                Code = room.Code,
                FloorNumber = room.FloorNumber,
                Available = room.Available,
                RoomTemplateSide = roomTemplate.Side,
                RoomTemplateWindows = roomTemplate.Windows,
                HotelName = hotel.Name,
                HotelAllowsPets = hotel.AllowsPets
            };
        }
    }
}
