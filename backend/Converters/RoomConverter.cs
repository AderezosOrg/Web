using DTOs;
using Entities;
using IConverters;
using System.Collections.Generic;
using System.Linq;

namespace Converters
{
    public class RoomConverter : IConverter1To5<Room, RoomTemplate, Hotel, List<BedInformation>, List<RoomBathInformation>, RoomDTO>
    {
        public RoomDTO Convert(Room room, RoomTemplate roomTemplate, Hotel hotel, List<BedInformation> bedInformations, List<RoomBathInformation> roomBathInformations)
        {
            var bedList = bedInformations
                .Where(b => b.RoomTemplateID == room.RoomTemplateID)
                .Select(b => b.BedID)
                .ToList();

            var bathList = roomBathInformations
                .Where(b => b.RoomTemplateID == room.RoomTemplateID)
                .Select(b => b.BathRoomID)
                .ToList();

            return new RoomDTO
            {
                RoomID = room.RoomID,
                Code = room.Code,
                FloorNumber = room.FloorNumber,
                Available = room.Available,
                RoomTemplateSide = roomTemplate.Side,
                RoomTemplateWindows = roomTemplate.Windows,
                HotelName = hotel.Name,
                HotelAllowsPets = hotel.AllowsPets,
                Beds = bedList,
                Bathrooms = bathList
            };
        }
    }
}
