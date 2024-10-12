using DTOs.WithoutId;
namespace backend.Services.AbstractClass;

public abstract class RoomService
{
    public abstract RoomPostDTO GetRoomById(Guid roomId); //
    public abstract List<RoomPostDTO> GetRooms(); //
    public abstract bool EditRoomAvailabilityById(Guid roomId, bool available); //
    public abstract bool CreateRoom(RoomPostDTO roomDto); //
    public abstract List<BedPostDTO> GetRoomBedsById(Guid roomId);
    public abstract List<BathroomPostDTO> GetRoomBathroomsById(Guid roomId);
    public abstract List<RoomPostDTO> GetAvailableRooms(); //
    public abstract List<RoomPostDTO> GetRoomsByFloor(int floorNumber);
    public abstract List<RoomPostDTO> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice);
    public abstract List<ServicePostDTO> GetRoomServicesById(Guid roomId); //x
}