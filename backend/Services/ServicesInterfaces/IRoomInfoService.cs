using DTOs.WithId;

namespace backend.Services.ServicesInterfaces;

public interface IRoomInfoService
{
    
    public Task<List<BedDTO>> GetRoomBedsById(Guid roomId);
    public Task<List<BathroomDTO>> GetRoomBathroomsById(Guid roomId);
    public Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId);
}