using backend.MyHappyBD;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using backend.Services.ServicesInterfaces;

namespace backend.Services;

public class RoomFiltersService: IRoomFiltersService
{
    private SingletonBD _singletonBd;
    private ReservationService _reservationService;
    private RoomConverter _roomConverter = new RoomConverter();
    private BathRoomService _bathRoomService;
    private ServiceService _serviceService;
    private BedService _bedService;
    private RoomService _roomService;

    public RoomFiltersService(ServiceService serviceService, ReservationService reservationService, BathRoomService bathRoomService, BedService bedService, RoomService roomService)
    {
        _serviceService = serviceService;
        _reservationService = reservationService;
        _bathRoomService = bathRoomService;
        _bedService = bedService;
        _roomService = roomService;
        _singletonBd = SingletonBD.Instance;

    }
    public async Task<List<RoomFullInfoDTO>> GetAvailableRooms(AvailabilityRequestDTO availabilityRequest)
    {
        List<RoomDTO> rooms = new List<RoomDTO>();
        foreach (Room room in _singletonBd.GetAllRooms())
        {
            if ( await IsAvailable(room, availabilityRequest))
            {
                var roomTemplate = _singletonBd.GetAllRoomTemplates().FirstOrDefault(r => r.RoomTemplateID == room.RoomTemplateID);
                var hotel = _singletonBd.GetAllHotels().FirstOrDefault(h => h.HotelID == room.HotelID);
                var bedInformation = _singletonBd.GetAllBedInformation().Where(b => b.RoomTemplateID == room.RoomTemplateID).ToList();
                var bathInformation = _singletonBd.GetAllBathroomInformation().Where(b => b.RoomTemplateID == room.RoomTemplateID).ToList();
                var serviceInformation = _singletonBd.GetAllRoomServices().Where(s => s.RoomID == room.RoomID).ToList();
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

        var roomsOnFloor = _singletonBd.GetAllRooms()
            .Where(r => r.FloorNumber == floorNumber)
            .ToList();

        var roomDTOs = roomsOnFloor.Select(r =>
        {
            var roomTemplate = _singletonBd.GetAllRoomTemplates().FirstOrDefault(rt => rt.RoomTemplateID == r.RoomTemplateID);
            var hotel = _singletonBd.GetAllHotels().FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformations = _singletonBd.GetAllBedInformation().Where(bi => bi.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomBathInformations = _singletonBd.GetAllBathroomInformation().Where(rb => rb.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomServices = _singletonBd.GetAllRoomServices().Where(rs => rs.RoomID == r.RoomID).ToList();

            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformations, roomBathInformations, roomServices);
        }).ToList();

        return roomDTOs;
    }


    public async Task<List<RoomDTO>> GetRoomsByPriceRange(PriceRangeRequestDTO priceRangeRequest)
    {
        await Task.Delay(10);

        var roomsInPriceRange = _singletonBd.GetAllRooms()
            .Where(r => r.PricePerNight >= priceRangeRequest.MinPrice && r.PricePerNight <= priceRangeRequest.MaxPrice)
            .ToList();

        var roomDTOs = roomsInPriceRange.Select(r =>
        {
            var roomTemplate = _singletonBd.GetAllRoomTemplates().FirstOrDefault(rt => rt.RoomTemplateID == r.RoomTemplateID);
            var hotel = _singletonBd.GetAllHotels().FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformations = _singletonBd.GetAllBedInformation().Where(bi => bi.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomBathInformations = _singletonBd.GetAllBathroomInformation().Where(rb => rb.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomServices = _singletonBd.GetAllRoomServices().Where(rs => rs.RoomID == r.RoomID).ToList();

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
