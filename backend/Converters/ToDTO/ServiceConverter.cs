namespace Converters.ToDTO;

using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

public class ServiceConverter : IConverter1To1<Service, ServiceDTO>
{
    public ServiceDTO Convert(Service service)
    {
        return new ServiceDTO
        {
            ServiceID = service.ServiceID,
            Type = service.Type
        };
    }
    
}