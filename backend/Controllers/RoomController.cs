using DTOs.WithId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    [HttpGet("{roomId}")]
    public ActionResult<RoomDTO> GetRoomById(Guid roomId)
    {
        return Ok(new RoomDTO
            {
                Bathrooms = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Beds = new List<Guid>
                {
                    
                },
                Code = "12m",
                FloorNumber = 0,
                HotelAllowsPets = false,
                HotelName = "Hotel",
                RoomID = Guid.NewGuid(),
                RoomTemplateSide = "east",
                RoomTemplateWindows = 3,
                Services = new List<Guid>
                {
                    Guid.NewGuid()
                },
                PricePerNight = 20m
            }
        );
    }
    
    [HttpGet]
    public ActionResult<List<RoomDTO>> GetRooms()
    {
        return Ok(new List<RoomDTO>
        {
            new RoomDTO
            {
                Bathrooms = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Beds = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Code = "12m",
                FloorNumber = 0,
                HotelAllowsPets = false,
                HotelName = "Hotel",
                RoomID = Guid.NewGuid(),
                RoomTemplateSide = "east",
                RoomTemplateWindows = 3,
                Services = new List<Guid>
                {
                    Guid.NewGuid()
                },
                PricePerNight = 20m
            }
        });
    }
    
    [HttpPatch("{roomId}")]
    public ActionResult<bool> EditRoomAvailabilityById(Guid roomId)
    {
        return Ok(true);
    }
    
    [HttpPost]
    public ActionResult<bool> CreateRoom(RoomDTO roomDto)
    {
        return Ok(true);
    }
    
    [HttpGet("beds/{roomId}")]
    public ActionResult<List<BedDTO>> GetRoomBedsById(Guid roomId)
    {
        return Ok(new List<BedDTO>
        {
            new BedDTO
            {
                BedID = Guid.NewGuid(),
                BedQuantity = 2,
                Capacity = "2",
                Size = "king"
            }
        });
    }
    
    [HttpGet("bathrooms/{roomId}")]
    public ActionResult<List<BathroomDTO>> GetRoomBathRoomsById(Guid roomId)
    {
        return Ok(new List<BathroomDTO>
        {
            new BathroomDTO()
            {
                BathRoomID = Guid.NewGuid(),
                BathroomQuantity = 2,
                Shower = false,
                DressingTable = true,
                Toilet = false
            }
        });
    }
    
    [HttpGet("availalble")]
    public ActionResult<List<RoomDTO>> GetAvailableRooms()
    {
        return Ok(new List<RoomDTO>
        {
            new RoomDTO
            {
                Bathrooms = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Beds = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Code = "2m",
                FloorNumber = 0,
                HotelAllowsPets = false,
                HotelName = "Hotel",
                RoomID = Guid.NewGuid(),
                RoomTemplateSide = "west",
                RoomTemplateWindows = 3,
                Services = new List<Guid>
                {
                    Guid.NewGuid()
                },
                PricePerNight = 23m
            }
        });
    }
    
    [HttpGet("floor/{floorNumber}")]
    public ActionResult<List<RoomDTO>> GetRoomsByFloor(int floorNumber)
    {
        return Ok(new List<RoomDTO>
        {
            new RoomDTO
            {
                Bathrooms = new List<Guid>
                {
                    
                },
                Beds = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Code = "2m",
                FloorNumber = 0,
                HotelAllowsPets = false,
                HotelName = "Hotel",
                RoomID = Guid.NewGuid(),
                RoomTemplateSide = "west",
                RoomTemplateWindows = 3,
                Services = new List<Guid>
                {
                    Guid.NewGuid()
                },
                PricePerNight = 20m
            }
        });
    }
    
    [HttpGet("prices/{minPrice}/{maxPrice}")]
    public ActionResult<List<RoomDTO>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return Ok(new List<RoomDTO>
        {
            new RoomDTO
            {
                Bathrooms = new List<Guid>
                {
                    
                },
                Beds = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Code = "2m",
                FloorNumber = 0,
                HotelAllowsPets = true,
                HotelName = "Hotel",
                RoomID = Guid.NewGuid(),
                RoomTemplateSide = "west",
                RoomTemplateWindows = 3,
                Services = new List<Guid>
                {
                    Guid.NewGuid()
                },
                PricePerNight = 20m
            }
        });
    }

    [HttpGet("services/{roomId}")]
    public ActionResult<List<ServiceDTO>> GetRoomServicesById(Guid roomId)
    {
        return Ok(new List<ServiceDTO>
        {
            new ServiceDTO()
            {
                ServiceID = Guid.NewGuid(),
                Type = "breakfast to the room"
            }
        });
    }
    
}
