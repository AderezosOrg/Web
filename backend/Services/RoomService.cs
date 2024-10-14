using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class RoomService : AbstractRoomService
{
    private SingletonBD _singletonBd;
    private RoomConverter _roomConverter = new RoomConverter();
    private RoomPostDTOConvert _roomPostDtoConvert = new RoomPostDTOConvert();
    private BedConverter _bedConverter = new BedConverter();
    private BathroomConverter _bathroomConverter = new BathroomConverter();
    private ServiceConverter _serviceConverter = new ServiceConverter();
    
    private readonly BedService _bedService;
    private readonly BathRoomServices _bathRoomServices;
    private readonly ReservationService _reservationService;
    private readonly ServiceService _serviceService;
    
    public RoomService()
    {
        _serviceService = new ServiceService();
        _reservationService = new ReservationService();
        _bathRoomServices = new BathRoomServices();
        _bedService = new BedService();
        _singletonBd = SingletonBD.Instance;

    }
    public override async Task<RoomPostDTO> GetRoomById(Guid roomId)
    {
        await Task.Delay(10);
        var room = _singletonBd.GetRoomById(roomId);
        if (room == null)
            throw new Exception("Room not found");
        var roomTemplate = _singletonBd.GetAllRoomTemplates().FirstOrDefault(r => r.RoomTemplateID == room.RoomTemplateID);
        var hotel = _singletonBd.GetAllHotels().FirstOrDefault(h => h.HotelID == room.HotelID);
        var bedInformation = _singletonBd.GetAllBedInformation().Where(b => b.RoomTemplateID == room.RoomTemplateID).ToList();
        var bathInformation = _singletonBd.GetAllBathroomInformation().Where(b => b.RoomTemplateID == room.RoomTemplateID).ToList();
        var serviceInformation = _singletonBd.GetAllRoomServices().Where(s => s.RoomID == room.RoomID).ToList();
        return _roomPostDtoConvert.Convert(room, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
    }

    public override async Task<List<RoomFullInfoDTO>> GetRooms()
    {
        await Task.Delay(10);
        List<RoomDTO> roomDtos = _singletonBd.GetAllRooms().Select(r =>
        {
            var roomTemplate = _singletonBd.GetAllRoomTemplates().FirstOrDefault(r => r.RoomTemplateID == r.RoomTemplateID);
            var hotel = _singletonBd.GetAllHotels().FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformation = _singletonBd.GetAllBedInformation().Where(b => b.RoomTemplateID == r.RoomTemplateID).ToList();
            var bathInformation = _singletonBd.GetAllBathroomInformation().Where(b => b.RoomTemplateID == r.RoomTemplateID).ToList();
            var serviceInformation = _singletonBd.GetAllRoomServices().Where(s => s.RoomID == r.RoomID).ToList();
            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
        }).ToList();
        
        List<RoomFullInfoDTO> fullInfoRooms = new List<RoomFullInfoDTO>();
        List<BathroomPostDTO> bathrooms = new List<BathroomPostDTO>();
        List<BedPostDTO> beds = new List<BedPostDTO>();
        List<ServicePostDTO> services = new List<ServicePostDTO>();

        foreach (var room in roomDtos)
        {
            foreach (var bathroomId in room.Bathrooms)
            {
                bathrooms.Add( await _bathRoomServices.GetBathRoomById(bathroomId));
            }
            foreach (var bedId in room.Beds)
            {
                beds.Add( await _bedService.GetBedById(bedId));
            }
            foreach (var serviceId in room.Services)
            {
                services.Add( await  _serviceService.GetServiceById(serviceId));
            }
            fullInfoRooms.Add(_roomConverter.Convert(room, bathrooms, beds, services));
            beds.Clear();
            bathrooms.Clear();
            services.Clear();
        }
        return fullInfoRooms;
    }

    public override async Task<RoomPostDTO> CreateRoom(RoomNewPostDTO roomPostDto)
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
            _singletonBd.AddRoom(room);
            var roomTemplate = _singletonBd.GetRoomTemplateById(room.RoomTemplateID);
            var hotel = _singletonBd.GetHotelById(room.HotelID);
            var bathroomInfo = _singletonBd.GetBathRoomInformationByRoomTemplateId(room.RoomTemplateID);
            var bedInfo = _singletonBd.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
            var services = _singletonBd.GetRoomServicesByRoomId(room.RoomID);
            return _roomPostDtoConvert.Convert(room, roomTemplate, hotel,bedInfo, bathroomInfo, services);
        }
        throw new Exception("Room not data found");
    }

    public override async Task<List<BedDTO>> GetRoomBedsById(Guid roomId)
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
            var bedPostDTO = await _bedService.GetBedById(bed.BedID);
            bedDtoList.Add(_bedConverter.Convert(bedPostDTO, bed.BedID));
        }

        return bedDtoList;
    }

    public override async Task<List<BathroomDTO>> GetRoomBathroomsById(Guid roomId)
    {
        await Task.Delay(10);

        var room = _singletonBd.GetRoomById(roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bathRoomInfo = _singletonBd.GetBathRoomInformationByRoomTemplateId(room.RoomTemplateID);
        var baths = new List<Bathroom>();
        foreach (RoomBathInformation bathInformation in bathRoomInfo)
        {
            baths.Add(_singletonBd.GetBathRoomById(bathInformation.BathRoomID));
        }
        var bathroomDto = new List<BathroomDTO>();
    
        foreach (var bath in baths)
        {
            var bathPostDto = await _bathRoomServices.GetBathRoomById(bath.BathRoomID);
            bathroomDto.Add(_bathroomConverter.Convert(bathPostDto, bath.BathRoomID));
        }

        return bathroomDto;
    }

    public override async Task<List<RoomFullInfoDTO>> GetAvailableRooms(DateTime startDate, DateTime endDate)
    {
        List<RoomDTO> rooms = new List<RoomDTO>();
        foreach (Room room in _singletonBd.GetAllRooms())
        {
            if ( await IsAvailable(room.RoomID, startDate, endDate))
            {
                rooms.Add(_roomConverter.Convert( await GetRoomById(room.RoomID), room.RoomID));
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
                bathrooms.Add( await _bathRoomServices.GetBathRoomById(bathroomId));
            }
            foreach (var bedId in room.Beds)
            {
                beds.Add( await _bedService.GetBedById(bedId));
            }
            foreach (var serviceId in room.Services)
            {
                services.Add( await  _serviceService.GetServiceById(serviceId));
            }
            fullInfoRooms.Add(_roomConverter.Convert(room, bathrooms, beds, services));
            beds.Clear();
            bathrooms.Clear();
            services.Clear();
        }
        
        return fullInfoRooms;
    }

    public override async Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber)
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


    public override async Task<List<RoomDTO>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        await Task.Delay(10);

        var roomsInPriceRange = _singletonBd.GetAllRooms()
            .Where(r => r.PricePerNight >= minPrice && r.PricePerNight <= maxPrice)
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


    public override async Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId)
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
            var servicePostDto = await _serviceService.GetServiceById(service.ServiceID);
            serviceDtos.Add(_serviceConverter.Convert(servicePostDto, service.ServiceID));
        }

        return serviceDtos;
    }

    public override async Task<bool> IsAvailable(Guid roomId, DateTime startDate, DateTime endDate)
    {
        List<ReservationDTO> reservationDtos = await _reservationService.GetReservationsByRoomId(roomId);
        foreach (ReservationDTO reservationDto in reservationDtos)
        {
            if (!(reservationDto.ReservationDate >= endDate || reservationDto.UseDate <= startDate) && !reservationDto.Cancelled)
            {
                return false;
            }
        }

        return true;
    }
}
