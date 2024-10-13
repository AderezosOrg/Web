using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services.AbstractClass;

public abstract class AbstractServiceService
{
    public abstract Task<ServicePostDTO> GetServiceById(Guid serviceId);
    public abstract Task<List<ServiceDTO>> GetServices();
    public abstract Task<ServicePostDTO> CreateService(ServicePostDTO serviceDto);
    public abstract Task<ServicePostDTO> ChangeServiceType(Guid serviceId, ServicePostDTO serviceDto);
}
