using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Db;

namespace backend.Services;

public class ServiceService : 
    IGetAllElementsService<ServiceDTO>,
    IGetElementById<ServicePostDTO>,
    ICreateSingleElement<ServicePostDTO, ServicePostDTO>,
    IUpdateElementByID<ServicePostDTO, ServicePostDTO>
{
    private ServiceDAO _serviceDao;
    
    private ServicePostConverter _servicePostConverter = new ServicePostConverter();
    private ServiceConverter _serviceConverter = new ServiceConverter();

    public ServiceService(ServiceDAO serviceDao)
    {
        _serviceDao = serviceDao;
    }
    
    public async Task<ServicePostDTO> GetElementById(Guid serviceId)
    {
        await Task.Delay(10);
        var service = _serviceDao.Read(serviceId);
        if (service == null)
            throw new Exception("Service not found");
        return _servicePostConverter.Convert(service);
    }

    public async Task<List<ServiceDTO>> GetAllElements()
    {
        await Task.Delay(10);
        
        List<ServiceDTO> result = _serviceDao.ReadAll().Select(s =>
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
            _serviceDao.Create(newService);
            return servicePostDto;
        }
        throw new Exception("Service not data found");
    }

    public async Task<ServicePostDTO> UpdateElementById(Guid serviceId, ServicePostDTO servicePostDto)
    {
       await Task.Delay(10);
       var service = new Service()
       {
           ServiceID = serviceId,
           Type = servicePostDto.Type,
       };
       _serviceDao.Update(service);
       return _servicePostConverter.Convert(service);
    }
}
