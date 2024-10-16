using DTOs.WithId;
using DTOs.WithoutId;
namespace backend.Services.AbstractClass;

public interface IRoomService
{
    public Task<List<BedDTO>> GetRoomBedsById(Guid roomId);
    public Task<List<BathroomDTO>> GetRoomBathroomsById(Guid roomId);
    public Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId);
    
}