using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Services.ServicesInterfaces;
using Db;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace backend.Services;

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IDAO<Session> _sessionDAO;
    private IDAO<Contact> _contactDAO;


    public SessionService(IHttpContextAccessor httpContextAccessor, IDAO<Session> sessionDao, IDAO<Contact> contactDao)
    {
        _httpContextAccessor = httpContextAccessor;
        _sessionDAO = sessionDao;
        _contactDAO = contactDao;
    }

    public async Task<SessionFullInfoDTO> PostSession(SessionPostDTO sessionPostDto)
    {
        Guid contactID = Guid.NewGuid();
        _contactDAO.Create(new Contact()
        {
            ContactID = contactID,
            Email = sessionPostDto.Email,
            PhoneNumber = ""
        });
        Guid sessionID = Guid.NewGuid();
        _sessionDAO.Create(new Session()
        {
            SessionID = sessionID,
            Token = sessionPostDto.Token,
            ContactID = contactID
        });
        return new SessionFullInfoDTO()
        {
            ContactID = contactID,
            Email = sessionPostDto.Email,
            PhoneNumber = "",
            SessionID = sessionID,
            Token = sessionPostDto.Token
        };
        
    }
    
    public async Task<bool> GetCookie(Guid sessionId)
    {
        Session session = _sessionDAO.Read(sessionId);
        Contact contact = _contactDAO.Read(session.ContactID);
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(session.Token));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, contact.Email),
            new Claim(JwtRegisteredClaimNames.PhoneNumber, contact.PhoneNumber),
            new Claim("ContactId", contact.ContactID.ToString()),
            new Claim("SessionId", session.SessionID.ToString()),
            new Claim( "Token", session.Token),
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
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(1), // Expiración en 1 día
            SameSite = SameSiteMode.None
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("session", tokenHandler.WriteToken(token), cookieOptions);
        return true;
    }

    public async Task<SessionFullInfoDTO> UpdateSession(SessionFullInfoDTO sessionFullInfoDto)
    {
        _sessionDAO.Update(new Session()
        {
            SessionID = sessionFullInfoDto.SessionID,
            ContactID = sessionFullInfoDto.ContactID,
            Token = sessionFullInfoDto.Token,
        });
        _contactDAO.Update(new Contact()
        {
            ContactID = sessionFullInfoDto.ContactID,
            Email = sessionFullInfoDto.Email,
            PhoneNumber = sessionFullInfoDto.PhoneNumber,
        });
        return sessionFullInfoDto;
    }

    public async Task<SessionFullInfoDTO> RefreshToken(SessionDTO sessionDto)
    {
        var oldSession = _sessionDAO.Read(sessionDto.SessionId);
        _sessionDAO.Update(new Session()
        {
            SessionID = oldSession.SessionID,
            ContactID = oldSession.ContactID,
            Token = sessionDto.Token
        });
        var contact = _contactDAO.Read(oldSession.ContactID);
        return new SessionFullInfoDTO()
        {
            ContactID = contact.ContactID,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            SessionID = oldSession.SessionID,
            Token = oldSession.Token
        };

    }

    public async Task<SessionFullInfoDTO> GetSession(Guid sessionId)
    {
        Session session = _sessionDAO.Read(sessionId);
        Contact contact = _contactDAO.Read(session.ContactID);
        return new SessionFullInfoDTO()
        {
            SessionID = session.SessionID,
            ContactID = contact.ContactID,
            Token = session.Token,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber
        };
    }

    public async Task<List<SessionDTO>> GetSessions()
    {
        var sessions = _sessionDAO.ReadAll().Select(s => new SessionDTO()
        {
            SessionId = s.SessionID,
            Token = s.Token
        }).ToList();
        return sessions;
    }
}
