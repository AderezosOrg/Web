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
        List<BedDTO> beds = await _roomService.GetRoomBedsById(roomId);
        return Ok(beds);
    }
    
    [HttpGet("bathrooms/{roomId}")]
    public async Task<ActionResult<List<BathroomDTO>>> GetRoomBathRoomsById(Guid roomId)
    {
        List<BathroomDTO> bathrooms = await _roomService.GetRoomBathroomsById(roomId);
        return Ok(bathrooms);
    }
    
    [HttpGet("available/{startDate}/{endDate}")]
    public async Task<ActionResult<List<RoomDTO>>> GetAvailableRooms(DateTime startDate, DateTime endDate)
    {
        List<RoomDTO> roomDtos = await _roomService.GetAvailableRooms(startDate, endDate);
        return Ok(roomDtos);
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
        List<ServiceDTO> services = await _roomService.GetRoomServicesById(roomId);
        return Ok(services);
        
    }
    
}
