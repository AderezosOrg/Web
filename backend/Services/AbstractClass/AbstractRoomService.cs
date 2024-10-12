using DTOs.WithId;
using DTOs.WithoutId;
namespace backend.Services.AbstractClass;

public abstract class AbstractRoomService
{
    public abstract Task<RoomPostDTO> GetRoomById(Guid roomId); //
    public abstract Task<List<RoomDTO>> GetRooms(); //
    public abstract Task<RoomPostDTO> CreateRoom(RoomPostDTO roomDto); //
    public abstract Task<List<BedPostDTO>> GetRoomBedsById(Guid roomId);
    public abstract Task<List<BathroomPostDTO>> GetRoomBathroomsById(Guid roomId);
    public abstract Task<List<RoomDTO>> GetAvailableRooms(DateTime startDate, DateTime endDate); //
    public abstract Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber);
    public abstract Task<List<RoomDTO>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice);
    public abstract Task<List<ServicePostDTO>> GetRoomServicesById(Guid roomId); //x
}