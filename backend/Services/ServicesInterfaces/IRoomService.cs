using DTOs.WithId;
using DTOs.WithoutId;
namespace backend.Services.ServicesInterfaces;

public interface IRoomService
{
    
    public Task<RoomPostDTO> CreateSingleElement(RoomNewPostDTO newElement);
    public Task<List<RoomFullInfoDTO>> GetAllElements();
    public Task<RoomFullInfoDTO> GetElementById(Guid elementId);
    
    
}
