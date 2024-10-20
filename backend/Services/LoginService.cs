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
    private readonly IHttpContextAccessor _httpContextAccessor;


    public LoginService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _logins = new List<LoginDTO>()
        {
            new LoginDTO()
            {
                Email = "admin@admin.com",
                Token = "ft3uyf131uof3fyuo11dofuo12fcdssc"
            }
        };
    }

    public async Task<bool> PostToken(LoginDTO loginDto)
    {
        _logins.Add(loginDto);
        Console.WriteLine(_logins[0].Token + _logins[0].Email);
        return true;
        
    }

    public async Task<bool> GetCookie()
    {
        Console.WriteLine(_logins[0].Token + _logins[0].Email);
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_logins[0].Token));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, _logins[0].Email),
            new Claim(JwtRegisteredClaimNames.PhoneNumber, "91398398"),
            new Claim( JwtRegisteredClaimNames.Jti, _logins[0].Token),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = false, 
            Secure = false,
            Expires = DateTime.UtcNow.AddDays(1), // Expiración en 1 día
            SameSite = SameSiteMode.None
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("session", tokenHandler.WriteToken(token), cookieOptions);
        return true;
    }
}