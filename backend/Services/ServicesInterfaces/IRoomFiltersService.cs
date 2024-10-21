using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services.ServicesInterfaces;

public interface IRoomFiltersService
{
    public Task<List<RoomFullInfoDTO>> GetAvailableRooms(AvailabilityRequestDTO availabilityRequest);
    public Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber);
    public Task<List<RoomDTO>> GetRoomsByPriceRange(PriceRangeRequestDTO priceRangeRequest);
    public Task<bool> IsAvailable(Room roomId,AvailabilityRequestDTO availabilityRequest);
    public Task<RoomFullInfoDTO> GetRandomAvailableRoom(AvailabilityRequestDTO availabilityRequest);
}
