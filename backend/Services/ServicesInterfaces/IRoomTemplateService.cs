using Converters.ToDTO;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IRoomTemplateService
{
    public Task<RoomTemplatePostDTO> CreateSingleElement(RoomTemplatePostDTO newElement);
    public Task<List<RoomTemplateDTO>> GetAllElements();
    public Task<RoomTemplatePostDTO> UpdateElementById(Guid elementId, RoomTemplatePostDTO updateElement);
    public Task<RoomTemplateDTO> GetElementById(Guid elementId);
}