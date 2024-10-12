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
    private BedPostConverter _bedPostConverter = new BedPostConverter();
    
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

    public override async Task<List<BedPostDTO>> GetRoomBedsById(Guid roomId)
    {
        await Task.Delay(10);
    
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bedInformationList = _beds.Where(bi => bi.RoomTemplateID == room.RoomTemplateID).ToList();
        var bedList = _beds.Where(b => bedInformationList.Select(bi => bi.BedID).Contains(b.BedID)).ToList();
        var bedPostDtoList = new List<BedPostDTO>();
    
        foreach (var bed in bedList)
        {
            var bedPostDTO = await _bedService.GetBedById(bed.BedID); 
            bedPostDtoList.Add(bedPostDTO);
        }

        return bedPostDtoList;
    }

    public override async Task<List<BathroomPostDTO>> GetRoomBathroomsById(Guid roomId)
    {
        await Task.Delay(10);
    
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");

        var bathInfomration = _roomBaths.Where(bi => bi.RoomTemplateID == room.RoomTemplateID).ToList();
        var bathList = _roomBaths.Where(b => bathInfomration.Select(bi => bi.BathRoomID).Contains(b.BathRoomID)).ToList();
        var bathPostDTOList = new List<BathroomPostDTO>();
    
        foreach (var bath in bathList)
        {
            var bathroomPostDTO = await _bathRoomServices.GetBathRoomById(bath .BathRoomID); 
            bathPostDTOList.Add(bathroomPostDTO);
        }

        return bathPostDTOList;
    }

    public override async Task<List<RoomDTO>> GetAvailableRooms(DateTime startDate, DateTime endDate)
    {
        await Task.Delay(10);
        var reservations = await _reservationService.GetReservations();
        var reservedRooms = reservations.Where(r => !r.Cancelled && 
                        ((r.UseDate >= startDate && r.UseDate <= endDate) && 
                         (r.ReservationDate >= startDate && r.ReservationDate <= endDate)))
            .Select(r => r.RoomID)
            .ToList();
        List<RoomDTO> availableRoomDTOs = new List<RoomDTO>();
        foreach (var room in reservedRooms)
        {
            
        }
        return availableRoomDTOs;
    } //HYUGO LO TERMINA :3

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


    public override async Task<List<ServicePostDTO>> GetRoomServicesById(Guid roomId)
    {
        await Task.Delay(10);
    
        var room = _rooms.FirstOrDefault(r => r.RoomID == roomId);
        if (room == null)
            throw new Exception("Room not found");

        var serviceInformation = _services.Where(bi => bi.ServiceID == room.RoomID).ToList();
        var serviceList = _services.Where(b => serviceInformation.Select(bi => bi.ServiceID).Contains(b.ServiceID)).ToList();
        var servicePostDTOList = new List<ServicePostDTO>();
    
        foreach (var service in serviceList)
        {
            var servicePostDTO = await _serviceService.GetServiceById(service .ServiceID); 
            servicePostDTOList.Add(servicePostDTO);
        }

        return servicePostDTOList;
    }
}
