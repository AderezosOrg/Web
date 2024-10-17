using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class RoomService : 
    IGetAllElementsService<RoomFullInfoDTO>,
    IGetElementById<RoomFullInfoDTO>,
    ICreateSingleElement<RoomNewPostDTO, RoomPostDTO>
{
    private SingletonBD _singletonBd;
    private RoomConverter _roomConverter = new RoomConverter();
    private RoomPostDTOConvert _roomPostDtoConvert = new RoomPostDTOConvert();
    private BedPostConverter _bedPostConverter = new BedPostConverter();
    private BathroomPostConverter _bathroomPostConverter = new BathroomPostConverter();
    private ServicePostConverter _servicePostConverter = new ServicePostConverter();
    
    private readonly BedService _bedService;
    private readonly ServiceService _serviceService;
    
    public RoomService()
    {
        _serviceService = new ServiceService();
        _bedService = new BedService();
        _singletonBd = SingletonBD.Instance;
    }
    public async Task<RoomFullInfoDTO> GetElementById(Guid roomId)
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
        var roomDto = _roomConverter.Convert(room, roomTemplate, hotel, bedInformation, bathInformation, serviceInformation);
        var bathPostDtos = new List<BathroomPostDTO>();
        foreach (RoomBathInformation information in bathInformation)
        {
            bathPostDtos.Add(_bathroomPostConverter.Convert(_singletonBd.GetBathRoomById(information.BathRoomID)));
        }
        var bedPostDtos  = new List<BedPostDTO>();
        foreach (BedInformation information in bedInformation)
        {
            bedPostDtos.Add(_bedPostConverter.Convert(_singletonBd.GetBedById(information.BedID)));
        }
        var servicePostDtos  = new List<ServicePostDTO>();
        foreach (RoomServices information in serviceInformation)
        {
            servicePostDtos.Add(_servicePostConverter.Convert(_singletonBd.GetServiceById(information.ServiceID)));
        }

        return _roomConverter.Convert(roomDto, bathPostDtos, bedPostDtos, servicePostDtos);
    }

    public async Task<List<RoomFullInfoDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<RoomDTO> roomDtos = _singletonBd.GetAllRooms().Select(r =>
        {
            var roomTemplate = _singletonBd.GetAllRoomTemplates().FirstOrDefault(rt => rt.RoomTemplateID == r.RoomTemplateID);
            var hotel = _singletonBd.GetAllHotels().FirstOrDefault(h => h.HotelID == r.HotelID);
            var bedInformation = _singletonBd.GetBedInformationByRoomTemplateId(r.RoomTemplateID);
            var bathInformation = _singletonBd.GetBathRoomInformationByRoomTemplateId(r.RoomTemplateID);
            var serviceInformation = _singletonBd.GetRoomServicesByRoomId(r.RoomID);
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
                
                bathrooms.Add(_bathroomPostConverter.Convert(_singletonBd.GetBathRoomById(bathroomId)));
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
            _singletonBd.AddRoom(room);
            var roomTemplate = _singletonBd.GetRoomTemplateById(room.RoomTemplateID);
            var hotel = _singletonBd.GetHotelById(room.HotelID);
            var bathroomInfo = _singletonBd.GetBathRoomInformationByRoomTemplateId(room.RoomTemplateID);
            var bedInfo = _singletonBd.GetBedInformationByRoomTemplateId(room.RoomTemplateID);
            foreach (Guid roomService in roomPostDto.RoomServices)
            {
                _singletonBd.AddRoomServices(new RoomServices()
                {
                    RoomID = room.RoomID,
                    ServiceID = roomService
                });
            }
            var services = _singletonBd.GetRoomServicesByRoomId(room.RoomID);
            return _roomPostDtoConvert.Convert(room, roomTemplate, hotel,bedInfo, bathroomInfo, services);
        }
        throw new Exception("Room not data found");
    }

    
}
