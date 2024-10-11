using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("{userId}")]
    public ActionResult<UserDTO> GetUserById(Guid userId)
    {
        return Ok(new UserDTO
        {
            CINumber = "6879821",
            Email = "user@email.com",
            HotelList = new List<Guid>
            {
                Guid.NewGuid()
            },
            Name = "user",
            PhoneNumber = "0123456789",
            UserID = Guid.NewGuid()
        });
    }
    
    [HttpGet]
    public ActionResult<List<UserDTO>> GetUsers()
    {
        return Ok(new List<UserDTO>
        {
            new UserDTO
            {
                CINumber = "6819821",
                Email = "user2@email.com",
                HotelList = new List<Guid>
                {
                    Guid.NewGuid()
                },
                Name = "userq qwe",
                PhoneNumber = "0023456789",
                UserID = Guid.NewGuid()
            }
        });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateUser(UserDTO userDto)
    {
        return true;
    }
    
    [HttpDelete("{userId}")]
    public ActionResult<bool> DeleteUserById(Guid userId)
    {
        return true;
    }
    
    [HttpPut("{userId}")]
    public ActionResult<bool> EditUserById(Guid userId, UserDTO userDto)
    {
        return true;
    }
}
