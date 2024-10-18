using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IUserService
{
    public Task<UserDTO> CreateSingleElement(UserPostDTO newElement);
    public Task<bool> DeleteElementById(Guid elementId);
    public Task<List<UserDTO>> GetAllElements();
    public Task<UserPostDTO> UpdateElementById(Guid elementId, UpdateUserDTO updateElement);
    public Task<UserPostDTO> GetElementById(Guid elementId);
}