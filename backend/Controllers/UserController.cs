using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISessionService _sessionService;

    public UserController(IUserService userService, ISessionService sessionService)
    {
        _userService = userService;
        _sessionService = sessionService;
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
    public async Task<ActionResult<UserDTO>> CreateUser(UserPostDTO userDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        UserDTO userPostDto = await _userService.CreateSingleElement(userDto);
        return Ok(userPostDto);
    }
    
    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> DeleteUserById(Guid userId)
    {
        bool result = await _userService.DeleteElementById(userId);
        return Ok(result);
    }
    
    [HttpPut("{userId}")]
    public async Task<ActionResult<UserPostDTO>> EditUserById(Guid userId, UpdateUserDTO userDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        UserPostDTO userPostDto = await _userService.UpdateElementById(userId, userDto);
        return Ok(userPostDto);
    }
}
