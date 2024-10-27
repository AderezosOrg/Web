using backend.Services;
using backend.Services.ServicesInterfaces;
using Db;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;

public class RoomFiltersServiceTests
{
    private readonly Mock<IDAO<Room>> _mockRoomDao;
    private readonly Mock<IDAO<RoomTemplate>> _mockRoomTemplateDao;
    private readonly Mock<IDAO<Hotel>> _mockHotelDao;
    private readonly Mock<IBedInformationDAO> _mockBedInformationDAO;
    private readonly Mock<IRoombathInformationDAO> _mockRoomBathInformationDAO;
    private readonly Mock<IRoomServicesDAO> _mockRoomServicesDAO;
    private readonly Mock<IReservationService> _mockReservationService;
    private readonly Mock<IBathRoomService> _mockBathRoomService;
    private readonly Mock<IServiceService> _mockServiceService;
    private readonly Mock<IBedService> _mockBedService;
    private readonly Mock<IRoomService> _mockRoomService;
    private readonly RoomFiltersService _roomFiltersService;

    public RoomFiltersServiceTests()
    {
        _mockRoomDao = new Mock<IDAO<Room>>();
        _mockRoomTemplateDao = new Mock<IDAO<RoomTemplate>>();
        _mockHotelDao = new Mock<IDAO<Hotel>>();
        _mockBedInformationDAO = new Mock<IBedInformationDAO>();
        _mockRoomBathInformationDAO = new Mock<IRoombathInformationDAO>();
        _mockRoomServicesDAO = new Mock<IRoomServicesDAO>();
        _mockReservationService = new Mock<IReservationService>();
        _mockBathRoomService = new Mock<IBathRoomService>();
        _mockServiceService = new Mock<IServiceService>();
        _mockBedService = new Mock<IBedService>();
        _mockRoomService = new Mock<IRoomService>();
        _roomFiltersService = new RoomFiltersService(
            _mockRoomDao.Object,
            _mockRoomTemplateDao.Object,
            _mockHotelDao.Object,
            _mockBedInformationDAO.Object,
            _mockRoomBathInformationDAO.Object,
            _mockRoomServicesDAO.Object,
            _mockServiceService.Object,
            _mockReservationService.Object,
            _mockBathRoomService.Object,
            _mockBedService.Object,
            _mockRoomService.Object
        );
    }

    [Fact]
    public async Task GetAvailableRooms_ReturnsAvailableRooms()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var roomTemplateId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();
        var room = new Room 
        { 
            RoomID = roomId, 
            RoomTemplateID = roomTemplateId, 
            HotelID = hotelId, 
            Code = "qw2", 
            PricePerNight = 24 
        };
        var availabilityRequest = new AvailabilityRequestDTO 
        { 
            StartDate = DateTime.Today, 
            EndDate = DateTime.Today.AddDays(2), 
            Capacity = 2 
        };
        var roomTemplate = new RoomTemplate { RoomTemplateID = roomTemplateId };
        var hotel = new Hotel { HotelID = hotelId };
        

        var bedId = Guid.NewGuid();
        var bed = new Bed
        {
            BedID = bedId,
            Capacity = 2,
            Size = "small"
        };
        
        var bathId = Guid.NewGuid();
        var bath = new Bathroom()
        {
            BathRoomID = bathId,
            DressingTable = true,
            Shower = true,
            Toilet = true
        };
        
        var serviceId = Guid.NewGuid();
        var service = new Service
        {
            ServiceID = serviceId,
            Type = "pets"
        };

        var roomBathInfo = new RoomBathInformation
        {
            BathRoomID = bathId,
            Quantity =  1, 
            RoomTemplateID = roomTemplateId
        };
        
        var bedInformation = new BedInformation
        {
            BedID = bedId,
            Quantity =  1, 
            RoomTemplateID = roomTemplateId
        };
        var roomService = new RoomServices
        {
            RoomID = roomId,
            ServiceID = serviceId,
        };
        // Setup mocks for RoomDao and associated services
        _mockRoomDao.Setup(dao => dao.ReadAll()).Returns(new List<Room> { room });
        _mockRoomService.Setup(service1 => service1.GetElementById(roomId)).ReturnsAsync(new RoomFullInfoDTO
        {
            RoomID = roomId,
            Bathrooms = new List<BathroomPostDTO>(),
            Beds = new List<BedPostDTO>
            {
                new BedPostDTO
                {
                    Capacity = 2, 
                    Size = "small"
                }
            },
            Code = room.Code,
            FloorNumber = room.FloorNumber,
            HotelAllowsPets = hotel.AllowsPets,
            HotelName = hotel.Name,
            PricePerNight = room.PricePerNight,
            RoomTemplateSide = roomTemplate.Side,
            RoomTemplateWindows = roomTemplate.Windows,
            Tax = hotel.Tax,
            Services = new List<ServicePostDTO>(),
        });
        _mockRoomTemplateDao.Setup(dao => dao.Read(roomTemplateId)).Returns(roomTemplate);
        _mockHotelDao.Setup(dao => dao.Read(hotelId)).Returns(hotel);
        _mockBedInformationDAO.Setup(dao => dao.GetBedInformationByRoomTemplateId(roomTemplateId)).Returns(new List<BedInformation> { bedInformation });
        _mockRoomBathInformationDAO.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(roomTemplateId)).Returns(new List<RoomBathInformation> { roomBathInfo });
        _mockRoomServicesDAO.Setup(dao => dao.GetRoomServicesByRoomId(roomId)).Returns(new List<RoomServices> { roomService });
        _mockReservationService.Setup(reservationService => reservationService.GetReservationsByRoomId(roomId)).ReturnsAsync(new List<ReservationDTO>());
        _mockBedService.Setup(bedService => bedService.GetElementById(bedId)).ReturnsAsync(new BedPostDTO
        {
            Capacity = bed.Capacity,
            Size = bed.Size,
        });
        _mockBathRoomService.Setup(bathRoomService => bathRoomService.GetElementById(bathId)).ReturnsAsync(new BathroomPostDTO()
        {
            DressingTable = bath.DressingTable,
            Shower = bath.Shower,
            Toilet = bath.Toilet
        });
        _mockServiceService.Setup(serviceService => serviceService.GetElementById(serviceId))
            .ReturnsAsync(new ServicePostDTO
            {
                Type = service.Type,
            });

        // Act
        var result = await _roomFiltersService.GetAvailableRooms(availabilityRequest);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetRoomsByFloor_ReturnsRoomsOnSpecifiedFloor()
    {
        // Arrange
        var floorNumber = 3;
        var room = new Room 
        { 
            RoomID = Guid.NewGuid(), 
            RoomTemplateID = Guid.NewGuid(), 
            HotelID = Guid.NewGuid(), 
            FloorNumber = floorNumber 
        };

        _mockRoomDao.Setup(dao => dao.ReadAll()).Returns(new List<Room> { room });
        var roomTemplate = new RoomTemplate { RoomTemplateID = room.RoomTemplateID };
        var hotel = new Hotel { HotelID = room.HotelID };

        _mockRoomTemplateDao.Setup(dao => dao.Read(room.RoomTemplateID)).Returns(roomTemplate);
        _mockHotelDao.Setup(dao => dao.Read(room.HotelID)).Returns(hotel);
        _mockBedInformationDAO.Setup(dao => dao.GetBedInformationByRoomTemplateId(room.RoomTemplateID)).Returns(new List<BedInformation>());
        _mockRoomBathInformationDAO.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID)).Returns(new List<RoomBathInformation>());
        _mockRoomServicesDAO.Setup(dao => dao.GetRoomServicesByRoomId(room.RoomID)).Returns(new List<RoomServices>());

        // Act
        var result = await _roomFiltersService.GetRoomsByFloor(floorNumber);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetRoomsByPriceRange_ReturnsRoomsInPriceRange()
    {
        // Arrange
        var minPrice = 50m;
        var maxPrice = 200m;
        var priceRangeRequest = new PriceRangeRequestDTO { MinPrice = minPrice, MaxPrice = maxPrice };
        var room = new Room 
        { 
            RoomID = Guid.NewGuid(), 
            RoomTemplateID = Guid.NewGuid(), 
            HotelID = Guid.NewGuid(), 
            PricePerNight = 100m 
        };

        _mockRoomDao.Setup(dao => dao.ReadAll()).Returns(new List<Room> { room });
        var roomTemplate = new RoomTemplate { RoomTemplateID = room.RoomTemplateID };
        var hotel = new Hotel { HotelID = room.HotelID };

        _mockRoomTemplateDao.Setup(dao => dao.Read(room.RoomTemplateID)).Returns(roomTemplate);
        _mockHotelDao.Setup(dao => dao.Read(room.HotelID)).Returns(hotel);
        _mockBedInformationDAO.Setup(dao => dao.GetBedInformationByRoomTemplateId(room.RoomTemplateID)).Returns(new List<BedInformation>());
        _mockRoomBathInformationDAO.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID)).Returns(new List<RoomBathInformation>());
        _mockRoomServicesDAO.Setup(dao => dao.GetRoomServicesByRoomId(room.RoomID)).Returns(new List<RoomServices>());

        // Act
        var result = await _roomFiltersService.GetRoomsByPriceRange(priceRangeRequest);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task IsAvailable_ReturnsTrueIfRoomIsAvailable()
    {
        // Arrange
        var room = new Room { RoomID = Guid.NewGuid(), RoomTemplateID = Guid.NewGuid(), HotelID = Guid.NewGuid(), Code = "qw2", PricePerNight = 24 };
        var availabilityRequest = new AvailabilityRequestDTO { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Capacity = 2 };
        var roomDto = new RoomFullInfoDTO
        {
            Beds = new List<BedPostDTO> { new BedPostDTO { Capacity = 2 } }
        };

        _mockRoomService.Setup(service => service.GetElementById(room.RoomID)).ReturnsAsync(roomDto);
        _mockBedInformationDAO.Setup(dao => dao.GetBedInformationByRoomTemplateId(room.RoomTemplateID)).Returns(new List<BedInformation>());
        _mockRoomBathInformationDAO.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(room.RoomTemplateID)).Returns(new List<RoomBathInformation>());
        _mockRoomTemplateDao.Setup(dao => dao.Read(room.RoomTemplateID)).Returns(new RoomTemplate());
        _mockHotelDao.Setup(dao => dao.Read(room.HotelID)).Returns(new Hotel());
        _mockRoomDao.Setup(dao => dao.ReadAll()).Returns(new List<Room> { room });
        _mockRoomServicesDAO.Setup(dao => dao.GetRoomServicesByRoomId(room.RoomID)).Returns(new List<RoomServices>());
        _mockReservationService.Setup(service => service.GetReservationsByRoomId(room.RoomID)).ReturnsAsync(new List<ReservationDTO>());

        // Act
        var result = await _roomFiltersService.IsAvailable(room, availabilityRequest);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetRandomAvailableRoom_ReturnsRandomAvailableRoom()
    {
        // Arrange
        // Arrange
        var roomId = Guid.NewGuid();
        var roomTemplateId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();
        var room = new Room 
        { 
            RoomID = roomId, 
            RoomTemplateID = roomTemplateId, 
            HotelID = hotelId, 
            Code = "qw2", 
            PricePerNight = 24 
        };
        var availabilityRequest = new AvailabilityRequestDTO 
        { 
            StartDate = DateTime.Today, 
            EndDate = DateTime.Today.AddDays(2), 
            Capacity = 2 
        };
        var roomTemplate = new RoomTemplate { RoomTemplateID = roomTemplateId };
        var hotel = new Hotel { HotelID = hotelId };
        

        var bedId = Guid.NewGuid();
        var bed = new Bed
        {
            BedID = bedId,
            Capacity = 2,
            Size = "small"
        };
        
        var bathId = Guid.NewGuid();
        var bath = new Bathroom()
        {
            BathRoomID = bathId,
            DressingTable = true,
            Shower = true,
            Toilet = true
        };
        
        var serviceId = Guid.NewGuid();
        var service = new Service
        {
            ServiceID = serviceId,
            Type = "pets"
        };

        var roomBathInfo = new RoomBathInformation
        {
            BathRoomID = bathId,
            Quantity =  1, 
            RoomTemplateID = roomTemplateId
        };
        
        var bedInformation = new BedInformation
        {
            BedID = bedId,
            Quantity =  1, 
            RoomTemplateID = roomTemplateId
        };
        var roomService = new RoomServices
        {
            RoomID = roomId,
            ServiceID = serviceId,
        };
        // Setup mocks for RoomDao and associated services
        _mockRoomDao.Setup(dao => dao.ReadAll()).Returns(new List<Room> { room });
        _mockRoomService.Setup(service1 => service1.GetElementById(roomId)).ReturnsAsync(new RoomFullInfoDTO
        {
            RoomID = roomId,
            Bathrooms = new List<BathroomPostDTO>(),
            Beds = new List<BedPostDTO>
            {
                new BedPostDTO
                {
                    Capacity = 2, 
                    Size = "small"
                }
            },
            Code = room.Code,
            FloorNumber = room.FloorNumber,
            HotelAllowsPets = hotel.AllowsPets,
            HotelName = hotel.Name,
            PricePerNight = room.PricePerNight,
            RoomTemplateSide = roomTemplate.Side,
            RoomTemplateWindows = roomTemplate.Windows,
            Tax = hotel.Tax,
            Services = new List<ServicePostDTO>(),
        });
        _mockRoomTemplateDao.Setup(dao => dao.Read(roomTemplateId)).Returns(roomTemplate);
        _mockHotelDao.Setup(dao => dao.Read(hotelId)).Returns(hotel);
        _mockBedInformationDAO.Setup(dao => dao.GetBedInformationByRoomTemplateId(roomTemplateId)).Returns(new List<BedInformation> { bedInformation });
        _mockRoomBathInformationDAO.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(roomTemplateId)).Returns(new List<RoomBathInformation> { roomBathInfo });
        _mockRoomServicesDAO.Setup(dao => dao.GetRoomServicesByRoomId(roomId)).Returns(new List<RoomServices> { roomService });
        _mockReservationService.Setup(reservationService => reservationService.GetReservationsByRoomId(roomId)).ReturnsAsync(new List<ReservationDTO>());
        _mockBedService.Setup(bedService => bedService.GetElementById(bedId)).ReturnsAsync(new BedPostDTO
        {
            Capacity = bed.Capacity,
            Size = bed.Size,
        });
        _mockBathRoomService.Setup(bathRoomService => bathRoomService.GetElementById(bathId)).ReturnsAsync(new BathroomPostDTO()
        {
            DressingTable = bath.DressingTable,
            Shower = bath.Shower,
            Toilet = bath.Toilet
        });
        _mockServiceService.Setup(serviceService => serviceService.GetElementById(serviceId))
            .ReturnsAsync(new ServicePostDTO
            {
                Type = service.Type,
            });
        // Act
        var result = await _roomFiltersService.GetRandomAvailableRoom(availabilityRequest);

        // Assert
        Assert.NotNull(result);
    }
}