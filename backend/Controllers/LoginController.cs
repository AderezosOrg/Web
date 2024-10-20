using backend.Services;
using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpPost]
    public async Task<ActionResult<bool>> PostToken(LoginDTO loginDto)
    {
        return Ok( await _loginService.PostToken(loginDto));
    }

    [HttpGet]
    public async Task<ActionResult<bool>> GetCookie()
    {
        return Ok(_loginService.GetCookie());
    }
}