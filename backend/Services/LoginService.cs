using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace backend.Services;

public class LoginService : ILoginService
{
    private List<LoginDTO> _logins;


    public LoginService()
    {
        
        _logins = new List<LoginDTO>();
    }

    public async Task<bool> PostToken(LoginDTO loginDto)
    {
        _logins.Add(loginDto);
        Console.WriteLine(_logins[0].Token + _logins[0].Email);
        return true;
        
    }

    public async Task<bool> GetCookie()
    {
        return true;
    }
}