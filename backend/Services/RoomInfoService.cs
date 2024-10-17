using backend.MyHappyBD;
using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using DTOs.WithId;
using Entities;

namespace backend.Services;

public class RoomInfoService : IRoomService
{
    private SingletonBD _singletonBd;
    private BedService _bedService;
    private BathRoomService _bathRoomService;
    private ServiceService _serviceService;
    private BedConverter _bedConverter;
    private BathroomConverter _bathroomConverter;
    private ServiceConverter _serviceConverter;

    public RoomInfoService(BedService bedService, BathRoomService bathRoomService, ServiceService serviceService)
    {
        _singletonBd = SingletonBD.Instance;
        _bedConverter = new BedConverter();
        _bathroomConverter = new BathroomConverter();
        _serviceConverter = new ServiceConverter();
        _bedService = bedService;
        _bathRoomService = bathRoomService;
        _serviceService = serviceService;
    }
    public async Task<List<BedDTO>> GetRoomBedsById(Guid roomId)
    {
        await Task.Delay(10);

        var room = _singletonBd.GetRoomById(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bedInformationList = _singletonBd.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
        var bedList = new List<Bed>();
        foreach (BedInformation bedInformation in bedInformationList)
        {
            bedList.Add(_singletonBd.GetBedById(bedInformation.BedID));
        }
        var bedDtoList = new List<BedDTO>();
    
        foreach (var bed in bedList)
        {
            var bedPostDTO = await _bedService.GetElementById(bed.BedID);
            bedDtoList.Add(_bedConverter.Convert(bedPostDTO, bed.BedID));
        }

        return bedDtoList;
    }

    public async Task<List<BathroomDTO>> GetRoomBathroomsById(Guid roomId)
    {
        await Task.Delay(10);

        var room = _singletonBd.GetRoomById(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bathRoomInfo = _singletonBd.GetBathRoomInformationByRoomTemplateId(room.RoomTemplateID);
        var bathroomDtos = new List<BathroomDTO>();
        foreach (RoomBathInformation bathInformation in bathRoomInfo)
        {
            var bath = _singletonBd.GetBathRoomById(bathInformation.BathRoomID);
            bathroomDtos.Add(_bathroomConverter.Convert(bath,bathInformation));
        }

        return bathroomDtos;
    }

    
    public async Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId)
    {
        
        await Task.Delay(10);

        var room = _singletonBd.GetRoomById(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var roomServicesList = _singletonBd.GetRoomServicesByRoomId(room.RoomID);
        var services = new List<Service>();
        foreach (RoomServices roomServices in roomServicesList)
        {
            services.Add(_singletonBd.GetServiceById(roomServices.ServiceID));
        }
        var serviceDtos = new List<ServiceDTO>();
    
        foreach (var service in services)
        {
            var servicePostDto = await _serviceService.GetElementById(service.ServiceID);
            serviceDtos.Add(_serviceConverter.Convert(servicePostDto, service.ServiceID));
        }

        return serviceDtos;
    }


}
