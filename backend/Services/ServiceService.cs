using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;

namespace backend.Services;

public class ServiceService : AbstractServiceService
{
    private static List<Service> _services = new List<Service>()
    {
        new Service()
        {
            Type = "Room Service"
        },
        new Service()
        {
            Type = "Food Service"
        }
    };
    
    private ServicePostConverter _servicePostConverter = new ServicePostConverter();

    public override async Task<ServicePostDTO> GetServiceById(Guid serviceId)
    {
        await Task.Delay(10);
        var service = _services.FirstOrDefault(s => s.ServiceID == serviceId);
        if (service == null)
            throw new Exception("Service not found");
        return _servicePostConverter.Convert(service);
    }

    public override async Task<List<ServicePostDTO>> GetServices()
    {
        await Task.Delay(10);
        List<ServicePostDTO> result = _services.Select(s =>
        {
            return _servicePostConverter.Convert(s);
        }).ToList();

        return result;
    }

    public override async Task<ServicePostDTO> CreateService(ServicePostDTO servicePostDto)
    {
        await Task.Delay(10);
        if (servicePostDto != null)
        {
            var newService = new Service
            {
                ServiceID = Guid.NewGuid(),
                Type = servicePostDto.Type,
            };
            _services.Add(newService);
            if(_services.Contains(newService))
                return servicePostDto;
            else
                throw new Exception("Service not created");
        }
        throw new Exception("Service not data found");
    }

    public override async Task<String> ChangeServiceType(Guid serviceId, string type)
    {
       await Task.Delay(10);
       var existingService = _services.FirstOrDefault(s => s.ServiceID == serviceId);
       if (existingService != null)
       {
           existingService.Type = type;
           return type;
       }
       throw new Exception("Service not found");
    }
}
