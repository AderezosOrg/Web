using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.Interfaces;

namespace backend.Services;

public class RoomService : IRoomService
{
    private List<RoomDTO> _rooms = new List<RoomDTO>
    {
        new RoomDTO
        {
            Available = true,
            Bathrooms = new List<Guid> { Guid.NewGuid() },
            Beds = new List<Guid> { Guid.NewGuid() },
            Code = "12m",
            FloorNumber = 0,
            HotelAllowsPets = false,
            HotelName = "Hotel",
            RoomID = Guid.NewGuid(),
            RoomTemplateSide = "east",
            RoomTemplateWindows = 3,
            Services = new List<Guid> { Guid.NewGuid() },
            PricePerNight = 20m
        }
    };

    public RoomDTO GetRoomById(Guid roomId)
    {
        return _rooms.FirstOrDefault(r => r.RoomID == roomId);
    }

    public List<RoomDTO> GetRooms()
    {
        return _rooms;
    }

    public bool EditRoomAvailabilityById(Guid roomId, bool available)
    {
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room != null)
        {
            room.Available = available;
            return true;
        }
        return false;
    }

    public bool CreateRoom(RoomDTO roomDto)
    {
        _rooms.Add(roomDto);
        return true;
    }

    public List<BedDTO> GetRoomBedsById(Guid roomId)
    {
        return new List<BedDTO>
        {
            new BedDTO
            {
                BedID = Guid.NewGuid(),
                BedQuantity = 2,
                Capacity = "2",
                Size = "king"
            }
        };
    }

    public List<BathroomDTO> GetRoomBathroomsById(Guid roomId)
    {
        return new List<BathroomDTO>
        {
            new BathroomDTO
            {
                BathRoomID = Guid.NewGuid(),
                BathroomQuantity = 2,
                Shower = false,
                DressingTable = true,
                Toilet = false
            }
        };
    }

    public List<RoomDTO> GetAvailableRooms()
    {
        return _rooms.Where(r => r.Available).ToList();
    }

    public List<RoomDTO> GetRoomsByFloor(int floorNumber)
    {
        return _rooms.Where(r => r.FloorNumber == floorNumber).ToList();
    }

    public List<RoomDTO> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return _rooms.Where(r => r.PricePerNight >= minPrice && r.PricePerNight <= maxPrice).ToList();
    }

    public List<ServiceDTO> GetRoomServicesById(Guid roomId)
    {
        return new List<ServiceDTO>
        {
            new ServiceDTO
            {
                ServiceID = Guid.NewGuid(),
                Type = "breakfast to the room"
            }
        };
    }
}
