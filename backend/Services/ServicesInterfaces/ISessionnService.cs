using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.ServicesInterfaces;

public interface ISessionService
{
    public Task<SessionFullInfoDTO> PostSession(SessionPostDTO sessionPostDto);
    public Task<bool> GetCookie(Guid sessionId);
    public Task<SessionFullInfoDTO> UpdateSession(SessionFullInfoDTO sessionFullInfoDto);
    public Task<SessionFullInfoDTO> RefreshToken(SessionDTO sessionDto);
    public Task<SessionFullInfoDTO> GetSession(Guid sessionId);
    public Task<List<SessionDTO>> GetSessions();
    public Task<bool> IsTokenValid(Guid sessionId);

}
