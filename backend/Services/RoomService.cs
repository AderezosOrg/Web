using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;

namespace backend.Services;

public class RoomService : AbstractRoomService
{
    private static List<Room> _rooms = new List<Room>()
    {
        new Room()
        {
            Code = "21a",
            FloorNumber = 1,
            HotelID = Guid.NewGuid(),
            PricePerNight = 2.0m,
            RoomID = Guid.NewGuid(),
            RoomTemplateID = Guid.NewGuid(),
        }
    };

    private List<RoomTemplate> _roomTemplates = new List<RoomTemplate>()
    {
        new RoomTemplate()
        {
            RoomTemplateID = _rooms[0].RoomTemplateID,
            Side = "left",
            Windows = 12
        }
    };

    private List<Hotel> _hotels = new List<Hotel>()
    {
        new Hotel()
        {
            HotelID = _rooms[0].HotelID,
            Address = "123 Main Street",
            AllowsPets = false,
            BathRoomID = Guid.NewGuid(),
            ContactID = Guid.NewGuid(),
            Name = "Hotel",
            Stars = 12,
            UserID = Guid.NewGuid(),
        }
    };

    private List<BedInformation> _beds = new List<BedInformation>()
    {
        new BedInformation()
        {
            RoomTemplateID = Guid.NewGuid(),
            BedID = Guid.NewGuid(),
            Quantity = 1
        }
    };

    private List<RoomBathInformation> _roomBaths = new List<RoomBathInformation>()
    {
        new RoomBathInformation()
        {
            RoomTemplateID = Guid.NewGuid(),
            BathRoomID = Guid.NewGuid(),
            Quantity = 1
        }
    };

    private List<RoomServices> _services = new List<RoomServices>()
    {
        new RoomServices()
        {
            ServiceID = Guid.NewGuid(),
            RoomID = Guid.NewGuid(),
        }
    };
    
    private RoomConverter _roomConverter = new RoomConverter();
    private RoomPostDTOConvert _roomPostDtoConvert = new RoomPostDTOConvert();
    private BedConverter _bedConverter = new BedConverter();
    private BathroomConverter _bathroomConverter = new BathroomConverter();
    private ServiceConverter _serviceConverter = new ServiceConverter();
    
    private readonly BedService _bedService;
    private readonly BathRoomServices _bathRoomServices;
    private readonly ReservationService _reservationService;
    private readonly ServiceService _serviceService;

    public RoomService(BathRoomServices bathRoomServices, BedService bedService, ReservationService reservationService
    , ServiceService serviceService)
    {
        _serviceService = serviceService;
        _reservationService = reservationService;
        _bathRoomServices = bathRoomServices;
        _bedService = bedService;
    }
    public RoomService()
    {
        _serviceService = new ServiceService();
        _reservationService = new ReservationService();
        _bathRoomServices = new BathRoomServices();
        _bedService = new BedService();
    }
    public override async Task<RoomPostDTO> GetRoomById(Guid roomId)
    {
        await Task.Delay(10);
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");
        var roomTemplate = _roomTemplates.FirstOrDefault(r => r.RoomTemplateID == room.RoomTemplateID);
        var hotel = _hotels.FirstOrDefault(h => h.HotelID == room.HotelID);
        var bedInformation = _beds.Where(b => b.RoomTemplateID == room.RoomTemplateID).ToList();
        var bathInformation = _roomBaths.Where(b => b.RoomTemplateID == room.RoomTemplateID).ToList();
        var serviceInformation = _services.Where(s => s.RoomID == room.RoomID).ToList();
        return _roomPostDtoConvert.Convert(room, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
    }

    public override async Task<List<RoomDTO>> GetRooms()
    {
        await Task.Delay(10);
        List<RoomDTO> result = _rooms.Select(r =>
        {
            var roomTemplate = _roomTemplates.FirstOrDefault(r => r.RoomTemplateID == r.RoomTemplateID);
            var hotel = _hotels.FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformation = _beds.Where(b => b.RoomTemplateID == r.RoomTemplateID).ToList();
            var bathInformation = _roomBaths.Where(b => b.RoomTemplateID == r.RoomTemplateID).ToList();
            var serviceInformation = _services.Where(s => s.RoomID == r.RoomID).ToList();
            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
        }).ToList();
        return result;
    }

    public override async Task<RoomPostDTO> CreateRoom(RoomPostDTO roomPostDto)
    {
        await Task.Delay(10);
        if (roomPostDto != null)
        {
            var newRoom = new Room
            {
                RoomID = Guid.NewGuid(),
                Code = roomPostDto.Code,
                FloorNumber = roomPostDto.FloorNumber,
                HotelID = Guid.NewGuid(),
                PricePerNight = roomPostDto.PricePerNight,
                RoomTemplateID = Guid.NewGuid(),
            };
            _rooms.Add(newRoom);

            var newHotel = new Hotel
            {
                HotelID = newRoom.HotelID,
                AllowsPets = roomPostDto.HotelAllowsPets,
                Name = roomPostDto.HotelName,
            };
            _hotels.Add(newHotel);

            var newTemplate = new RoomTemplate
            {
                RoomTemplateID = newRoom.RoomTemplateID,
                Side = roomPostDto.RoomTemplateSide,
                Windows = roomPostDto.RoomTemplateWindows,
            };
            _roomTemplates.Add(newTemplate);

            var bedInformation = new BedInformation
            {
                RoomTemplateID = newRoom.RoomTemplateID,
                BedID = Guid.NewGuid(),
            };
            _beds.Add(bedInformation);

            var bathroomInformation = new RoomBathInformation
            {
                RoomTemplateID = newRoom.RoomTemplateID,
                BathRoomID = Guid.NewGuid(),
            };
            _roomBaths.Add(bathroomInformation);

            var serviceInfomration = new RoomServices
            {
                ServiceID = Guid.NewGuid(),
                RoomID = newRoom.RoomID,
            };
            _services.Add(serviceInfomration);
            if(_rooms.Contains(newRoom) && _roomTemplates.Contains(newTemplate) && _hotels.Contains(newHotel) 
               && _beds.Contains(bedInformation) && _roomBaths.Contains(bathroomInformation) && _services.Contains(serviceInfomration))
                return roomPostDto;
            else
                throw new Exception("Room not created");
        }
        throw new Exception("Room not data found");
    }

    public override async Task<List<BedDTO>> GetRoomBedsById(Guid roomId)
    {
        await Task.Delay(10);
    
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bedInformationList = _beds.Where(bi => bi.RoomTemplateID == room.RoomTemplateID).ToList();
        var bedList = _beds.Where(b => bedInformationList.Select(bi => bi.BedID).Contains(b.BedID)).ToList();
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
    
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bathInfomration = _roomBaths.Where(bi => bi.RoomTemplateID == room.RoomTemplateID).ToList();
        var bathList = _roomBaths.Where(b => bathInfomration.Select(bi => bi.BathRoomID).Contains(b.BathRoomID)).ToList();
        var bathDTOList = new List<BathroomDTO>();
    
        foreach (var bath in bathList)
        {
            var bathroomDTO = await _bathRoomServices.GetBathRoomById(bath .BathRoomID); 
            bathDTOList.Add(_bathroomConverter.Convert(bathroomDTO, bath.BathRoomID));
        }

        return bathDTOList;
    }

    public override async Task<List<RoomDTO>> GetAvailableRooms(DateTime startDate, DateTime endDate)
    {
        List<RoomDTO> rooms = new List<RoomDTO>();
        foreach (Room room in _rooms)
        {
            if ( await IsAvailable(room.RoomID, startDate, endDate))
            {
                rooms.Add(_roomConverter.Convert( await GetRoomById(room.RoomID), room.RoomID));
            }
        }
        return rooms;
    }

    public override async Task<List<RoomDTO>> GetRoomsByFloor(int floorNumber)
    {
        await Task.Delay(10);

        var roomsOnFloor = _rooms
            .Where(r => r.FloorNumber == floorNumber)
            .ToList();

        var roomDTOs = roomsOnFloor.Select(r =>
        {
            var roomTemplate = _roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == r.RoomTemplateID);
            var hotel = _hotels.FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformations = _beds.Where(bi => bi.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomBathInformations = _roomBaths.Where(rb => rb.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomServices = _services.Where(rs => rs.RoomID == r.RoomID).ToList();

            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformations, roomBathInformations, roomServices);
        }).ToList();

        return roomDTOs;
    }


    public override async Task<List<RoomDTO>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        await Task.Delay(10);

        var roomsInPriceRange = _rooms
            .Where(r => r.PricePerNight >= minPrice && r.PricePerNight <= maxPrice)
            .ToList();

        var roomDTOs = roomsInPriceRange.Select(r =>
        {
            var roomTemplate = _roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == r.RoomTemplateID);
            var hotel = _hotels.FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformations = _beds.Where(bi => bi.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomBathInformations = _roomBaths.Where(rb => rb.RoomTemplateID == r.RoomTemplateID).ToList();
            var roomServices = _services.Where(rs => rs.RoomID == r.RoomID).ToList();

            return _roomConverter.Convert(r, roomTemplate, hotel, bedInformations, roomBathInformations, roomServices);
        }).ToList();

        return roomDTOs;
    }


    public override async Task<List<ServiceDTO>> GetRoomServicesById(Guid roomId)
    {
        await Task.Delay(10);
    
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");

        var serviceInformation = _services.Where(bi => bi.ServiceID == room.RoomID).ToList();
        var serviceList = _services.Where(b => serviceInformation.Select(bi => bi.ServiceID).Contains(b.ServiceID)).ToList();
        var serviceDTOList = new List<ServiceDTO>();
    
        foreach (var service in serviceList)
        {
            var servicePostDTO = await _serviceService.GetServiceById(service.ServiceID);
        }

        return serviceDTOList;
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
