using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IServiceService
{
    public Task<ServicePostDTO> CreateSingleElement(ServicePostDTO newElement);
    public Task<List<ServiceDTO>> GetAllElements();
    public Task<ServicePostDTO> UpdateElementById(Guid elementId, ServicePostDTO updateElement);
    public Task<ServicePostDTO> GetElementById(Guid elementId);
}
