using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    [HttpGet("{serviceId}")]
    public ActionResult<ServiceDTO> GetServiceById(Guid serviceId)
    {
        return Ok(new ServiceDTO
        {
            ServiceID = Guid.NewGuid(),
            Type = "Room Service"
        });
    }
    
    [HttpGet]
    public ActionResult<List<ServiceDTO>> GetServices()
    {
        return Ok(new List<ServiceDTO>
        {
            new ServiceDTO
            {
                ServiceID = Guid.NewGuid(),
                Type = "Room Service"
            }
        });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateService([FromBody] ServiceDTO serviceDto)
    {
        return true;
    }
    
    [HttpPatch("{serviceId}")]
    public ActionResult<bool> ChangeServiceType(Guid serviceId, string type)
    {
        return true;
    }
}
