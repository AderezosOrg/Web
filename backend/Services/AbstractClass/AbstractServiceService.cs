using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractServiceService
{
    public abstract Task<ServicePostDTO> GetServiceById(Guid serviceId);
    public abstract Task<List<ServicePostDTO>> GetServices();
    public abstract Task<ServicePostDTO> CreateService(ServicePostDTO serviceDto);
    public abstract Task<String> ChangeServiceType(Guid serviceId, string type);
}
