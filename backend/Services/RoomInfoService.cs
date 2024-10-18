using backend.MyHappyBD;
using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using Db;
using DTOs.WithId;
using Entities;

namespace backend.Services;

public class RoomInfoService : IRoomService
{
    private RoomDAO _roomDao;
    private BedInformationDAO _bedInformationDAO;
    private RoomBathInformationDAO _RoomBathInformationDAO;
    private RoomServicesDAO _RoomServicesDAO;
    private BathroomDAO _BathroomDAO;
    private BedDAO _bedDAO;
    private ServiceDAO _serviceDAO;
    private BedService _bedService;
    private ServiceService _serviceService;
    private BedConverter _bedConverter;
    private BathroomConverter _bathroomConverter;
    private ServiceConverter _serviceConverter;

    public RoomInfoService(BedService bedService, ServiceService serviceService, RoomDAO roomDao, BedInformationDAO bedInformationDao, RoomBathInformationDAO roomBathInformationDao, RoomServicesDAO roomServicesDao, BathroomDAO bathroomDao, BedDAO bedDao, ServiceDAO serviceDao)
    {
        _bedConverter = new BedConverter();
        _bathroomConverter = new BathroomConverter();
        _serviceConverter = new ServiceConverter();
        _bedService = bedService;
        _serviceService = serviceService;
        _roomDao = roomDao;
        _bedInformationDAO = bedInformationDao;
        _RoomBathInformationDAO = roomBathInformationDao;
        _RoomServicesDAO = roomServicesDao;
        _BathroomDAO = bathroomDao;
        _bedDAO = bedDao;
        _serviceDAO = serviceDao;
    }
    public async Task<List<BedDTO>> GetRoomBedsById(Guid roomId)
    {
        await Task.Delay(10);

        var room = _roomDao.Read(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bedInformationList = _bedInformationDAO.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
        var bedListDTO = new List<BedDTO>();
        foreach (BedInformation bedInformation in bedInformationList)
        {
            var bed = _bedDAO.Read(bedInformation.BedID);
            var bedPostDTO = await _bedService.GetElementById(bed.BedID);
            bedListDTO.Add(_bedConverter.Convert(bedPostDTO, bed.BedID));
        }

        return bedListDTO;
    }

    public async Task<List<BathroomDTO>> GetRoomBathroomsById(Guid roomId)
    {
        await Task.Delay(10);

        var room = _roomDao.Read(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bathRoomInfo = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID);
        var bathroomDtos = new List<BathroomDTO>();
        foreach (RoomBathInformation bathInformation in bathRoomInfo)
        {
            var bath = _BathroomDAO.Read(bathInformation.BathRoomID);
            bathroomDtos.Add(_bathroomConverter.Convert(bath,bathInformation));
        }

        return bathroomDtos;
    }

    
    public async Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId)
    {
        
        await Task.Delay(10);

        var room = _roomDao.Read(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var roomServicesList = _RoomServicesDAO.GetRoomServicesByRoomId(room.RoomID);
        var services = new List<Service>();
        foreach (RoomServices roomServices in roomServicesList)
        {
            services.Add(_serviceDAO.Read(roomServices.ServiceID));
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
