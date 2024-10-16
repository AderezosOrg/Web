using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services.AbstractClass;

public interface IRoomFiltersService
{
    public Task<List<RoomFullInfoDTO>> GetAvailableRooms(AvailabilityRequestDTO availabilityRequest);
    public Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber);
    public Task<List<RoomDTO>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice);
    public Task<bool> IsAvailable(Room roomId,AvailabilityRequestDTO availabilityRequest);
}
