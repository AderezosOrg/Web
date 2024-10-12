using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractUserService
{
    public abstract Task<UserPostDTO> GetUserById(Guid userId);
    public abstract Task<List<UserPostDTO>> GetUsers();
    public abstract Task<UserPostDTO> CreateUser(UserPostDTO userDto);
    public abstract Task<bool> DeleteUserById(Guid userId);
    public abstract Task<UserPostDTO> EditUserById(Guid userId, UserPostDTO userDto);
}
