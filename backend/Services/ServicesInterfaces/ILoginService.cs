using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.ServicesInterfaces;

public interface ILoginService
{
    public Task<bool> PostToken(LoginDTO loginDto);
    public Task<bool> GetCookie();
    
}
