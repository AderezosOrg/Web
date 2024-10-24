using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IHotelService
{
    
    public Task<HotelPostDTO> CreateSingleElement(HotelPostDTO newElement);
    public Task<List<HotelDTO>> GetAllElements();
    public Task<HotelPostDTO> UpdateElementById(Guid elementId, HotelPostDTO updateElement);
    public Task<HotelPostDTO> GetElementById(Guid elementId);
}
