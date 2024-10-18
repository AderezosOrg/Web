using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IBedService
{
    public Task<BedPostDTO> CreateSingleElement(BedPostDTO newElement);
    public Task<bool> DeleteElementById(Guid elementId);
    public Task<List<BedInfoDTO>> GetAllElements();
    public Task<BedPostDTO> UpdateElementById(Guid elementId, BedPostDTO updateElement);
    public Task<BedPostDTO> GetElementById(Guid elementId);
}