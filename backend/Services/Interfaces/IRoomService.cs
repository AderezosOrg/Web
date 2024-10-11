namespace backend.Services.Interfaces;
using DTOs;

public interface IRoomService
{
    RoomDTO GetRoomById(Guid roomId);
    List<RoomDTO> GetRooms();
    bool EditRoomAvailabilityById(Guid roomId, bool available);
    bool CreateRoom(RoomDTO roomDto);
    List<BedDTO> GetRoomBedsById(Guid roomId);
    List<BathroomDTO> GetRoomBathroomsById(Guid roomId);
    List<RoomDTO> GetAvailableRooms();
    List<RoomDTO> GetRoomsByFloor(int floorNumber);
    List<RoomDTO> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice);
    List<ServiceDTO> GetRoomServicesById(Guid roomId);
}
