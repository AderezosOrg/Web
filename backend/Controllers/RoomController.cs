using backend.Services;
using backend.Services.AbstractClass;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<RoomPostDTO>> GetRoomById(Guid roomId)
    {
        RoomPostDTO room = await _roomService.GetRoomById(roomId);
        return Ok(room);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetRooms()
    {
        List<RoomDTO> rooms = await _roomService.GetRooms();
        return Ok(rooms);
    }
    
    [HttpPost]
    public async Task<ActionResult<RoomPostDTO>> CreateRoom(RoomPostDTO roomDto)
    {
        RoomPostDTO room = await _roomService.CreateRoom(roomDto);
        return Ok(room);
    }
    
    [HttpGet("beds/{roomId}")]
    public async Task<ActionResult<List<BedDTO>>> GetRoomBedsById(Guid roomId)
    {
        List<BedPostDTO> beds = await _roomService.GetRoomBedsById(roomId);
        return Ok(beds);
    }
    
    [HttpGet("bathrooms/{roomId}")]
    public async Task<ActionResult<List<BathroomDTO>>> GetRoomBathRoomsById(Guid roomId)
    {
        List<BathroomPostDTO> beds = await _roomService.GetRoomBathroomsById(roomId);
        return Ok(beds);
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
    public async Task<ActionResult<List<RoomDTO>>> GetRoomsByFloor(int floorNumber)
    {
        List<RoomDTO> rooms = await _roomService.GetRoomsByFloor(floorNumber);
        return Ok(rooms);
    }
    
    [HttpGet("prices/{minPrice}/{maxPrice}")]
    public async Task<ActionResult<List<RoomDTO>>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        List<RoomDTO> rooms = await _roomService.GetRoomsByPriceRange(minPrice, maxPrice);
        return Ok(rooms);
    }

    [HttpGet("services/{roomId}")]
    public async Task<ActionResult<List<ServiceDTO>>> GetRoomServicesById(Guid roomId)
    {
        List<ServicePostDTO> services = await _roomService.GetRoomServicesById(roomId);
        return Ok(services);
        
    }
    
}
