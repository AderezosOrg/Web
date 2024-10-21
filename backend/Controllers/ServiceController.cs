using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;
    private readonly ISessionService _sessionService;

    public ServiceController(IServiceService serviceService, ISessionService sessionService)
    {
        _serviceService = serviceService;
        _sessionService = sessionService;
    }

    [HttpGet("{serviceId}")]
    public async Task<ActionResult<ServicePostDTO>> GetServiceById(Guid serviceId)
    {
        ServicePostDTO serviceById = await _serviceService.GetElementById(serviceId);
        return Ok(serviceById);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ServiceDTO>>> GetServices()
    {
        List<ServiceDTO> services = await _serviceService.GetAllElements();
        return Ok(services);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServicePostDTO>> CreateService([FromBody] ServicePostDTO serviceDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        ServicePostDTO service = await _serviceService.CreateSingleElement(serviceDto);
        return Ok(service);
    }
    
    [HttpPatch("{serviceId}")]
    public async Task<ActionResult<ServicePostDTO>> ChangeServiceType(Guid serviceId, ServicePostDTO type)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        ServicePostDTO service = await _serviceService.UpdateElementById(serviceId, type);
        return Ok(service);
    }
}
