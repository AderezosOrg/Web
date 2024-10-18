using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IContactService
{
    
    public Task<ContactDTO> CreateSingleElement(ContactPostDTO newElement);
    public Task<List<ContactDTO>> GetAllElements();
    public Task<ContactDTO> UpdateElementById(Guid elementId, ContactPostDTO updateElement);
    public Task<ContactPostDTO> GetElementById(Guid elementId);
}