namespace backend.Services.Interfaces;
using DTOs;

public interface IServiceService
{
    ServiceDTO GetServiceById(Guid serviceId);
    List<ServiceDTO> GetServices();
    bool CreateService(ServiceDTO serviceDto);
    bool ChangeServiceType(Guid serviceId, string type);
}
