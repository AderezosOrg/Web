using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class ServiceService : AbstractServiceService
{
    private SingletonBD _singletonBd;
    
    private ServicePostConverter _servicePostConverter = new ServicePostConverter();
    private ServiceConverter _serviceConverter = new ServiceConverter();

    public ServiceService()
    {
        _singletonBd = SingletonBD.Instance;
    }
    
    public override async Task<ServicePostDTO> GetServiceById(Guid serviceId)
    {
        await Task.Delay(10);
        var service = _singletonBd.GetServiceById(serviceId);
        if (service == null)
            throw new Exception("Service not found");
        return _servicePostConverter.Convert(service);
    }

    public override async Task<List<ServiceDTO>> GetServices()
    {
        await Task.Delay(10);
        
        List<ServiceDTO> result = _singletonBd.GetAllServices().Select(s =>
        {
            return _serviceConverter.Convert(s);
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
            _singletonBd.AddService(newService);
            if(_singletonBd.GetAllServices().Contains(newService))
                return servicePostDto;
            else
                throw new Exception("Service not created");
        }
        throw new Exception("Service not data found");
    }

    public override async Task<ServicePostDTO> ChangeServiceType(Guid serviceId, ServicePostDTO servicePostDto)
    {
       await Task.Delay(10);
       return _servicePostConverter.Convert(_singletonBd.UpdateService(new Service()
       {
           ServiceID = serviceId,
           Type = servicePostDto.Type,
       }));
    }
}
