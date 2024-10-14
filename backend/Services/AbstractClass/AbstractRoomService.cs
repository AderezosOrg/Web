using DTOs.WithId;
using DTOs.WithoutId;
namespace backend.Services.AbstractClass;

public abstract class AbstractRoomService
{
    public abstract Task<RoomPostDTO> GetRoomById(Guid roomId);
    public abstract Task<List<RoomFullInfoDTO>> GetRooms();
    public abstract Task<RoomPostDTO> CreateRoom(RoomPostDTO roomDto);
    public abstract Task<List<BedDTO>> GetRoomBedsById(Guid roomId);
    public abstract Task<List<BathroomDTO>> GetRoomBathroomsById(Guid roomId);
    public abstract Task<List<RoomFullInfoDTO>> GetAvailableRooms(DateTime startDate, DateTime endDate);
    public abstract Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber);
    public abstract Task<List<RoomDTO>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice);
    public abstract Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId);
    public abstract Task<bool> IsAvailable(Guid roomId, DateTime startDate, DateTime endDate);
}