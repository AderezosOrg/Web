using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IBathRoomService
{
    public Task<BathroomPostDTO> CreateSingleElement(BathroomPostDTO newElement);
    public Task<bool> DeleteElementById(Guid elementId);
    public Task<List<BathroomInfoDTO>> GetAllElements();
    public Task<BathroomPostDTO> UpdateElementById(Guid elementId, BathroomPostDTO updateElement);
    public Task<BathroomPostDTO> GetElementById(Guid elementId);

}
