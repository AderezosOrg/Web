using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class ServicePostConverter : IConverter1To1<Service, ServicePostDTO>
{
    public ServicePostDTO Convert(Service service)
    {
        return new ServicePostDTO
        {
            Type = service.Type
        };
    }
    
}