using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserPostDTO>> GetUserById(Guid userId)
    {
        UserPostDTO userPostDto = await _userService.GetElementById(userId);
        return Ok(userPostDto);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsers()
    {
        List<UserDTO> userPostDto = await _userService.GetAllElements();
        return Ok(userPostDto);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserPostDTO>> CreateUser(UserPostDTO userDto)
    {
        UserPostDTO userPostDto = await _userService.CreateSingleElement(userDto);
        return Ok(userPostDto);
    }
    
    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> DeleteUserById(Guid userId)
    {
        bool result = await _userService.DeleteElementById(userId);
        return Ok(result);
    }
    
    [HttpPut("{userId}")]
    public async Task<ActionResult<UserPostDTO>> EditUserById(Guid userId, UserPostDTO userDto)
    {
        UserPostDTO userPostDto = await _userService.UpdateElementById(userId, userDto);
        return Ok(userPostDto);
    }
}
