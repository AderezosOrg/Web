using backend.Services;
using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _loginService;
    public SessionController(ISessionService loginService)
    {
        _loginService = loginService;
    }
    
    [HttpPost]
    public async Task<ActionResult<SessionFullInfoDTO>> PostToken(SessionPostDTO sessionPostDto)
    {
        SessionFullInfoDTO sessionFullInfoDTO = await _loginService.PostSession(sessionPostDto);
        return Ok(sessionFullInfoDTO);
    }

    [HttpGet("cookie/{sessionId}")]
    public async Task<IActionResult> GetCookie(Guid sessionId)
    {
        await _loginService.GetCookie(sessionId);
        return  Redirect("http://localhost:5173/personal-info");
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SessionDTO>>> GetSessions()
    {

        return Ok(await _loginService.GetSessions());
    }
    
    [HttpPut("token")]
    public async Task<ActionResult<SessionFullInfoDTO>> RefreshToken(SessionDTO sessionDto)
    {
        return await _loginService.RefreshToken(sessionDto);
    }
    
    [HttpPut]
    public async Task<ActionResult<SessionFullInfoDTO>> UpdateSession(SessionFullInfoDTO sessionFullInfoDto)
    {
        return await _loginService.UpdateSession(sessionFullInfoDto);
    }
    
    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetSession(Guid sessionId)
    {
        var session = await _loginService.GetSession(sessionId);
        return Ok(session);
    }
}
