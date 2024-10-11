using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.Interfaces;
namespace backend.Services;

public class ServiceService : IServiceService
{
    private List<ServiceDTO> _services = new List<ServiceDTO>
    {
        new ServiceDTO
        {
            ServiceID = Guid.NewGuid(),
            Type = "Room Service"
        }
    };

    public ServiceDTO GetServiceById(Guid serviceId)
    {
        return _services.FirstOrDefault(s => s.ServiceID == serviceId);
    }

    public List<ServiceDTO> GetServices()
    {
        return _services;
    }

    public bool CreateService(ServiceDTO serviceDto)
    {
        _services.Add(serviceDto);
        return true;
    }

    public bool ChangeServiceType(Guid serviceId, string type)
    {
        var service = _services.FirstOrDefault(s => s.ServiceID == serviceId);
        if (service != null)
        {
            service.Type = type;
            return true;
        }
        return false;
    }
}
