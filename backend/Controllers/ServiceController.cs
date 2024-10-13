using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _serviceService;

    public ServiceController(ServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet("{serviceId}")]
    public async Task<ActionResult<ServicePostDTO>> GetServiceById(Guid serviceId)
    {
        ServicePostDTO serviceById = await _serviceService.GetServiceById(serviceId);
        return Ok(serviceById);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ServiceDTO>>> GetServices()
    {
        List<ServiceDTO> services = await _serviceService.GetServices();
        return Ok(services);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServicePostDTO>> CreateService([FromBody] ServicePostDTO serviceDto)
    {
        ServicePostDTO service = await _serviceService.CreateService(serviceDto);
        return Ok(service);
    }
    
    [HttpPatch("{serviceId}")]
    public async Task<ActionResult<ServicePostDTO>> ChangeServiceType(Guid serviceId, ServicePostDTO type)
    {
        ServicePostDTO service = await _serviceService.ChangeServiceType(serviceId, type);
        return Ok(service);
    }
}
