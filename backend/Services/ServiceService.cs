using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class ServiceService : 
    IGetAllElementsService<ServiceDTO>,
    IGetElementById<ServicePostDTO>,
    ICreateSingleElement<ServicePostDTO, ServicePostDTO>,
    IUpdateElementByID<ServicePostDTO, ServicePostDTO>
{
    private SingletonBD _singletonBd;
    
    private ServicePostConverter _servicePostConverter = new ServicePostConverter();
    private ServiceConverter _serviceConverter = new ServiceConverter();

    public ServiceService()
    {
        _singletonBd = SingletonBD.Instance;
    }
    
    public async Task<ServicePostDTO> GetElementById(Guid serviceId)
    {
        await Task.Delay(10);
        var service = _singletonBd.GetServiceById(serviceId);
        if (service == null)
            throw new Exception("Service not found");
        return _servicePostConverter.Convert(service);
    }

    public async Task<List<ServiceDTO>> GetAllElements()
    {
        await Task.Delay(10);
        
        List<ServiceDTO> result = _singletonBd.GetAllServices().Select(s =>
        {
            return _serviceConverter.Convert(s);
        }).ToList();

        return result;
    }

    public async Task<ServicePostDTO> CreateSingleElement(ServicePostDTO servicePostDto)
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
            return servicePostDto;
        }
        throw new Exception("Service not data found");
    }

    public async Task<ServicePostDTO> UpdateElementById(Guid serviceId, ServicePostDTO servicePostDto)
    {
       await Task.Delay(10);
       return _servicePostConverter.Convert(_singletonBd.UpdateService(new Service()
       {
           ServiceID = serviceId,
           Type = servicePostDto.Type,
       }));
    }
}
