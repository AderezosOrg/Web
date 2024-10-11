using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.Interfaces;

namespace backend.Services;

public class UserService : IUserService
{
    private List<UserDTO> _users = new List<UserDTO>
    {
        new UserDTO
        {
            UserID = Guid.NewGuid(),
            Name = "user1",
            Email = "user1@email.com",
            PhoneNumber = "0123456789",
            CINumber = "6879821",
            HotelList = new List<Guid> { Guid.NewGuid() }
        }
    };

    public UserDTO GetUserById(Guid userId)
    {
        return _users.FirstOrDefault(u => u.UserID == userId);
    }

    public List<UserDTO> GetUsers()
    {
        return _users;
    }

    public bool CreateUser(UserDTO userDto)
    {
        _users.Add(userDto);
        return true;
    }

    public bool DeleteUserById(Guid userId)
    {
        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user != null)
        {
            _users.Remove(user);
            return true;
        }
        return false;
    }

    public bool EditUserById(Guid userId, UserDTO userDto)
    {
        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user != null)
        {
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.CINumber = userDto.CINumber;
            user.HotelList = userDto.HotelList;
            return true;
        }
        return false;
    }
}
