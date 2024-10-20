using backend.MyHappyBD;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using backend.Services.ServicesInterfaces;
using Db;

namespace backend.Services;

public class RoomFiltersService: IRoomFiltersService
{
    private IDAO<Room> _roomDao;
    private RoomTemplateDAO _roomTemplateDao;
    private IDAO<Hotel> _hotelDao;
    private BedInformationDAO _bedInformationDAO;
    private RoomBathInformationDAO _RoomBathInformationDAO;
    private RoomServicesDAO _RoomServicesDAO; 
    
    private IReservationService _reservationService;
    private RoomConverter _roomConverter = new RoomConverter();
    private IBathRoomService _bathRoomService;
    private IServiceService _serviceService;
    private IBedService _bedService;
    private IRoomService _roomService;

    public RoomFiltersService(IDAO<Room> roomDao, RoomTemplateDAO roomTemplateDao, IDAO<Hotel> hotelDao, BedInformationDAO bedInformationDao,
        RoomBathInformationDAO roomBathInformation, RoomServicesDAO roomServicesDao ,IServiceService serviceService, IReservationService reservationService,
        IBathRoomService bathRoomService, IBedService bedService, IRoomService roomService)
    {
        _serviceService = serviceService;
        _reservationService = reservationService;
        _bathRoomService = bathRoomService;
        _bedService = bedService;
        _roomService = roomService;
        _roomDao = roomDao;
        _roomTemplateDao = roomTemplateDao;
        _hotelDao = hotelDao;
        _bedInformationDAO = bedInformationDao;
        _RoomBathInformationDAO = roomBathInformation;
        _RoomServicesDAO = roomServicesDao;
    }
    public async Task<List<RoomFullInfoDTO>> GetAvailableRooms(AvailabilityRequestDTO availabilityRequest)
    {
        List<RoomDTO> rooms = new List<RoomDTO>();
        foreach (Room room in _roomDao.ReadAll())
        {
            if ( await IsAvailable(room, availabilityRequest))
            {
                var roomTemplate = _roomTemplateDao.Read(room.RoomTemplateID);
                var hotel = _hotelDao.Read(room.HotelID);
                var bedInformation = _bedInformationDAO.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
                var bathInformation = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID);
                var serviceInformation = _RoomServicesDAO.GetRoomServicesByRoomId(room.RoomID);
                rooms.Add(_roomConverter.Convert(room, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation));
            }
        }
        List<RoomFullInfoDTO> fullInfoRooms = new List<RoomFullInfoDTO>();
        List<BathroomPostDTO> bathrooms = new List<BathroomPostDTO>();
        List<BedPostDTO> beds = new List<BedPostDTO>();
        List<ServicePostDTO> services = new List<ServicePostDTO>();

        foreach (var room in rooms)
        {
            foreach (var bathroomId in room.Bathrooms)
            {
                bathrooms.Add( await _bathRoomService.GetElementById(bathroomId));
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

    public async Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber)
    {
        await Task.Delay(10);

        var roomsOnFloor = _roomDao.ReadAll()
            .Where(r => r.FloorNumber == floorNumber)
            .ToList();

        var roomDTOs = roomsOnFloor.Select(r =>
        {
            var roomTemplate = _roomTemplateDao.Read(r.RoomTemplateID);
            var hotel = _hotelDao.Read(r.HotelID);
            var bedInformations = _bedInformationDAO.GetBedInformationByRoomTemplateId(roomTemplate.RoomTemplateID);
            var roomBathInformations = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(roomTemplate.RoomTemplateID);
            var roomServices = _RoomServicesDAO.GetRoomServicesByRoomId(r.RoomID);

            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformations, roomBathInformations, roomServices);
        }).ToList();

        return roomDTOs;
    }


    public async Task<List<RoomDTO>> GetRoomsByPriceRange(PriceRangeRequestDTO priceRangeRequest)
    {
        await Task.Delay(10);

        var roomsInPriceRange = _roomDao.ReadAll()
            .Where(r => r.PricePerNight >= priceRangeRequest.MinPrice && r.PricePerNight <= priceRangeRequest.MaxPrice)
            .ToList();

        var roomDTOs = roomsInPriceRange.Select(r =>
        {
            var roomTemplate = _roomTemplateDao.Read(r.RoomTemplateID);
            var hotel =_hotelDao.Read(r.HotelID);
            var bedInformations = _bedInformationDAO.GetBedInformationByRoomTemplateId(roomTemplate.RoomTemplateID);
            var roomBathInformations = _RoomBathInformationDAO.GetRoombathInformationsByRoomTemplateId(roomTemplate.RoomTemplateID);
            var roomServices = _RoomServicesDAO.GetRoomServicesByRoomId(r.RoomID);

            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformations, roomBathInformations, roomServices);
        }).ToList();

        return roomDTOs;
    }
    
    public async Task<bool> IsAvailable(Room room, AvailabilityRequestDTO availabilityRequest)
    {
        List<ReservationDTO> reservationDtos = await _reservationService.GetReservationsByRoomId(room.RoomID);
        RoomFullInfoDTO roomDto = await _roomService.GetElementById(room.RoomID);
        int capacity = roomDto.Beds.Sum(b => b.Capacity);
        if (capacity < availabilityRequest.Capacity)
        {
            return false;
        }
        foreach (ReservationDTO reservationDto in reservationDtos)
        {
            
            if (!(reservationDto.ReservationDate >= availabilityRequest.EndDate || reservationDto.UseDate <= availabilityRequest.StartDate) && !reservationDto.Cancelled)
            {
                return false;
            }
        }

        return true;
    }


}
