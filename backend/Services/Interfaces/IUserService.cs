namespace backend.Services.Interfaces;
using DTOs;

public interface IUserService
{
    UserDTO GetUserById(Guid userId);
    List<UserDTO> GetUsers();
    bool CreateUser(UserDTO userDto);
    bool DeleteUserById(Guid userId);
    bool EditUserById(Guid userId, UserDTO userDto);
}
