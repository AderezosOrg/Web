using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Db;

namespace backend.Services;

public class RoomService : 
    IGetAllElementsService<RoomFullInfoDTO>,
    IGetElementById<RoomFullInfoDTO>,
    ICreateSingleElement<RoomNewPostDTO, RoomPostDTO>
{
    
    
    private IDAO<Room> _roomDao;
    private RoomTemplateDAO _roomTemplateDao;
    private IDAO<Hotel> _hotelDao;
    private BedInformationDAO _bedInformationDAO;
    private RoomBathInformationDAO _RoomBathInformationDAO;
    private RoomServicesDAO _RoomServicesDAO;
    private IDAO<Bathroom> _BathroomDAO;
    private IDAO<Bed> _bedDAO;
    private IDAO<Service> _serviceDAO;
    
    private RoomConverter _roomConverter = new RoomConverter();
    private RoomPostDTOConvert _roomPostDtoConvert = new RoomPostDTOConvert();
    private BedPostConverter _bedPostConverter = new BedPostConverter();
    private BathroomPostConverter _bathroomPostConverter = new BathroomPostConverter();
    private ServicePostConverter _servicePostConverter = new ServicePostConverter();
    
    private readonly BedService _bedService;
    private readonly ServiceService _serviceService;
    private readonly BathRoomService _bathRoomService;
    
    public RoomService(ServiceService serviceService, BedService bedService, BathRoomService bathRoomService, IDAO<Room> roomDao,
        RoomTemplateDAO roomTemplateDao, IDAO<Hotel> hotelDao, BedInformationDAO bedInformationDao,
        RoomBathInformationDAO roomBathInformationDao, RoomServicesDAO roomServicesDao, IDAO<Bathroom> bathroomDao, IDAO<Bed> bedDao, IDAO<Service> serviceDao)
    {
        _serviceService = serviceService;
        _bedService = bedService;
        _bathRoomService = bathRoomService;
        _roomDao = roomDao;
        _roomTemplateDao = roomTemplateDao;
        _hotelDao = hotelDao;
        _bedInformationDAO = bedInformationDao;
        _RoomBathInformationDAO = roomBathInformationDao;
        _RoomServicesDAO = roomServicesDao;
        _BathroomDAO = bathroomDao;
        _bedDAO = bedDao;
        _serviceDAO = serviceDao;
    }
    public async Task<RoomFullInfoDTO> GetElementById(Guid roomId)
    {
        await Task.Delay(10);
        var room = _roomDao.Read(roomId);
        if (room == null)
            throw new Exception("Room not found");
        var roomTemplate = _roomTemplateDao.ReadAll().FirstOrDefault(r => r.RoomTemplateID == room.RoomTemplateID);
        var hotel = _hotelDao.ReadAll().FirstOrDefault(h => h.HotelID == room.HotelID);
        var bedInformation = _bedInformationDAO.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
        var bathInformation = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID);
        var serviceInformation = _RoomServicesDAO.GetRoomServicesByRoomId(room.RoomID);
        var roomDto = _roomConverter.Convert(room, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
        var bathPostDtos = new List<BathroomPostDTO>();
        foreach (RoomBathInformation information in bathInformation)
        {
            bathPostDtos.Add(_bathroomPostConverter.Convert(_BathroomDAO.Read(information.BathRoomID)));
        }
        var bedPostDtos  = new List<BedPostDTO>();
        foreach (BedInformation information in bedInformation)
        {
            bedPostDtos.Add(_bedPostConverter.Convert(_bedDAO.Read(information.BedID)));
        }
        var servicePostDtos  = new List<ServicePostDTO>();
        foreach (RoomServices information in serviceInformation)
        {
            servicePostDtos.Add(_servicePostConverter.Convert(_serviceDAO.Read(information.ServiceID)));
        }

        return _roomConverter.Convert(roomDto, bathPostDtos, bedPostDtos, servicePostDtos);
    }

    public async Task<List<RoomFullInfoDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<RoomDTO> roomDtos = _roomDao.ReadAll().Select(room =>
        {
            var roomTemplate = _roomTemplateDao.ReadAll().FirstOrDefault(r => r.RoomTemplateID == room.RoomTemplateID);
            var hotel = _hotelDao.ReadAll().FirstOrDefault(h => h.HotelID == room.HotelID);
            var bedInformation = _bedInformationDAO.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
            var bathInformation = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID);
            var serviceInformation = _RoomServicesDAO.GetRoomServicesByRoomId(room.RoomID);
            return _roomConverter.Convert(room, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
        }).ToList();
        
        List<RoomFullInfoDTO> fullInfoRooms = new List<RoomFullInfoDTO>();
        List<BathroomPostDTO> bathrooms = new List<BathroomPostDTO>();
        List<BedPostDTO> beds = new List<BedPostDTO>();
        List<ServicePostDTO> services = new List<ServicePostDTO>();

        foreach (var room in roomDtos)
        {
            foreach (var bathroomId in room.Bathrooms)
            {
                
                bathrooms.Add(await _bathRoomService.GetElementById(bathroomId));
            }
            foreach (var bedId in room.Beds)
            {
                beds.Add( await _bedService.GetElementById(bedId));
            }
            foreach (var serviceId in room.Services)
            {
                services.Add( await  _serviceService.GetElementById(serviceId));
            }
            fullInfoRooms.Add(_roomConverter.Convert(room, bathrooms, beds, services));
            beds.Clear();
            bathrooms.Clear();
            services.Clear();
        }
        return fullInfoRooms;
    }

    public async Task<RoomPostDTO> CreateSingleElement(RoomNewPostDTO roomPostDto)
    {
        await Task.Delay(10);
        if (roomPostDto != null)
        {
            var room = new Room()
            {
                Code = roomPostDto.Code,
                FloorNumber = roomPostDto.FloorNumber,
                HotelID = roomPostDto.HotelId,
                PricePerNight = roomPostDto.PricePerNight,
                RoomID = Guid.NewGuid(),
                RoomTemplateID = roomPostDto.RoomTemplateId
            };
            _roomDao.Create(room);
            var roomTemplate = _roomTemplateDao.Read(room.RoomTemplateID);
            var hotel = _hotelDao.Read(room.HotelID);
            var bathroomInfo = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID);
            var bedInfo = _bedInformationDAO.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
            foreach (Guid roomService in roomPostDto.RoomServices)
            {
                _RoomServicesDAO.Create(new RoomServices()
                {
                    RoomID = room.RoomID,
                    ServiceID = roomService
                });
            }
            var services = _RoomServicesDAO.GetRoomServicesByRoomId(room.RoomID);
            return _roomPostDtoConvert.Convert(room, roomTemplate, hotel,bedInfo, bathroomInfo, services);
        }
        throw new Exception("Room not data found");
    }

    
}
