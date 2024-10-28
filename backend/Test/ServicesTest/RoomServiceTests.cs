using backend.Services;
using backend.Services.ServicesInterfaces;
using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;

public class RoomServiceTests
{
    private readonly RoomService _roomService;
    private readonly Mock<IDAO<Room>> _mockRoomDao;
    private readonly Mock<IDAO<RoomTemplate>> _mockRoomTemplateDao;
    private readonly Mock<IDAO<Hotel>> _mockHotelDao;
    private readonly Mock<IBedInformationDAO> _mockBedInformationDao;
    private readonly Mock<IRoombathInformationDAO> _mockRoomBathInformationDao;
    private readonly Mock<IRoomServicesDAO> _mockRoomServicesDao;
    private readonly Mock<IDAO<Bathroom>> _mockBathroomDao;
    private readonly Mock<IDAO<Bed>> _mockBedDao;
    private readonly Mock<IDAO<Service>> _mockServiceDao;
    private readonly Mock<IBedService> _mockBedService;
    private readonly Mock<IServiceService> _mockServiceService;
    private readonly Mock<IBathRoomService> _mockBathRoomService;

    public RoomServiceTests()
    {
        _mockRoomDao = new Mock<IDAO<Room>>();
        _mockRoomTemplateDao = new Mock<IDAO<RoomTemplate>>();
        _mockHotelDao = new Mock<IDAO<Hotel>>();
        _mockBedInformationDao = new Mock<IBedInformationDAO>();
        _mockRoomBathInformationDao = new Mock<IRoombathInformationDAO>();
        _mockRoomServicesDao = new Mock<IRoomServicesDAO>();
        _mockBathroomDao = new Mock<IDAO<Bathroom>>();
        _mockBedDao = new Mock<IDAO<Bed>>();
        _mockServiceDao = new Mock<IDAO<Service>>();
        _mockBedService = new Mock<IBedService>();
        _mockServiceService = new Mock<IServiceService>();
        _mockBathRoomService = new Mock<IBathRoomService>();

        _roomService = new RoomService(
            _mockServiceService.Object,
            _mockBedService.Object,
            _mockBathRoomService.Object,
            _mockRoomDao.Object,
            _mockRoomTemplateDao.Object,
            _mockHotelDao.Object,
            _mockBedInformationDao.Object,
            _mockRoomBathInformationDao.Object,
            _mockRoomServicesDao.Object,
            _mockBathroomDao.Object,
            _mockBedDao.Object,
            _mockServiceDao.Object);
    }

    [Fact]
    public async Task GetElementById_ShouldReturnRoom_WhenRoomExists()
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
            PricePerNight = 3,
            FloorNumber = 2,
            Code = "qwe"
        };
    
        var roomTemplate = new RoomTemplate { RoomTemplateID = roomTemplateId };
        var hotel = new Hotel { HotelID = hotelId };
    
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns(room);
        _mockRoomTemplateDao.Setup(dao => dao.ReadAll()).Returns(new List<RoomTemplate> { roomTemplate });
        _mockHotelDao.Setup(dao => dao.ReadAll()).Returns(new List<Hotel> { hotel });

        // Ensure other required DAOs return valid data as well
        _mockBedInformationDao.Setup(dao => dao.GetBedInformationByRoomTemplateId(roomTemplateId)).Returns(new List<BedInformation>());
        _mockRoomBathInformationDao.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(roomTemplateId)).Returns(new List<RoomBathInformation>());
        _mockRoomServicesDao.Setup(dao => dao.GetRoomServicesByRoomId(roomId)).Returns(new List<RoomServices>());

        // Act
        var result = await _roomService.GetElementById(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(roomId, result.RoomID);
    }

    [Fact]
    public async Task GetAllElements_ShouldReturnListOfRoomFullInfoDTO()
    {
        // Arrange
        var roomTemplateId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();
    
        var rooms = new List<Room>
        {
            new Room { RoomID = Guid.NewGuid(), RoomTemplateID = roomTemplateId, HotelID = hotelId },
            new Room { RoomID = Guid.NewGuid(), RoomTemplateID = roomTemplateId, HotelID = hotelId }
        };
    
        var roomTemplate = new RoomTemplate { RoomTemplateID = roomTemplateId };
        var hotel = new Hotel { HotelID = hotelId };
    
        _mockRoomDao.Setup(dao => dao.ReadAll()).Returns(rooms);
        _mockRoomTemplateDao.Setup(dao => dao.ReadAll()).Returns(new List<RoomTemplate> { roomTemplate });
        _mockHotelDao.Setup(dao => dao.ReadAll()).Returns(new List<Hotel> { hotel });

        // Ensure other required DAOs return valid data
        _mockBedInformationDao.Setup(dao => dao.GetBedInformationByRoomTemplateId(roomTemplateId)).Returns(new List<BedInformation>());
        _mockRoomBathInformationDao.Setup(dao => dao.GetRoombathInformationsByRoomTemplateId(roomTemplateId)).Returns(new List<RoomBathInformation>());
        _mockRoomServicesDao.Setup(dao => dao.GetRoomServicesByRoomId(It.IsAny<Guid>())).Returns(new List<RoomServices>());

        // Act
        var result = await _roomService.GetAllElements();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rooms.Count, result.Count);
    }

    [Fact]
    public async Task CreateSingleElement_ShouldReturnRoomPostDTO_WhenRoomIsCreated()
    {
        // Arrange
    var hotelId = Guid.NewGuid();
    var roomTemplateId = Guid.NewGuid();
    var roomPostDto = new RoomNewPostDTO
    {
        Code = "R001",
        FloorNumber = 1,
        HotelId = hotelId,
        PricePerNight = 100,
        RoomTemplateId = roomTemplateId,
        RoomServices = new List<Guid> { Guid.NewGuid() } // Add a service ID for testing
    };

    var hotel = new Hotel
    {
        HotelID = hotelId,
        Address = "123 Main Street",
        AllowsPets = true,
        BathRoomID = Guid.NewGuid(),
        ContactID = Guid.NewGuid(),
        Name = "Test Hotel",
        Stars = 3,
        Tax = 12,
        UserID = Guid.NewGuid(),
    };

    var roomTemplate = new RoomTemplate
    {
        RoomTemplateID = roomTemplateId,
        Side = "west",
        Windows = 2
    };

    // Mock the DAO return values
    _mockHotelDao.Setup(setDao => setDao.Read(hotelId)).Returns(hotel);
    _mockRoomTemplateDao.Setup(setDao => setDao.Read(roomTemplateId)).Returns(roomTemplate);
    _mockRoomDao.Setup(setDao => setDao.Create(It.IsAny<Room>())).Verifiable(); // Ensure the create method is called

    // Mock the methods that need to return a list
    _mockBedInformationDao.Setup(setDao => setDao.GetBedInformationByRoomTemplateId(roomTemplateId))
        .Returns(new List<BedInformation> { new BedInformation { BedID = Guid.NewGuid() } });
    
    _mockRoomBathInformationDao.Setup(setDao => setDao.GetRoombathInformationsByRoomTemplateId(roomTemplateId))
        .Returns(new List<RoomBathInformation> { new RoomBathInformation { BathRoomID = Guid.NewGuid() } });

    _mockRoomServicesDao.Setup(setDao => setDao.GetRoomServicesByRoomId(It.IsAny<Guid>()))
        .Returns(new List<RoomServices> { new RoomServices { ServiceID = Guid.NewGuid() } });

    // Act
    var result = await _roomService.CreateSingleElement(roomPostDto);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(roomPostDto.Code, result.Code);
    Assert.Equal(roomPostDto.FloorNumber, result.FloorNumber);
    _mockRoomDao.Verify(dao => dao.Create(It.IsAny<Room>()), Times.Once); // Verify that Create was called
    }

    [Fact]
    public async Task GetElementById_ShouldThrowException_WhenRoomNotFound()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _roomService.GetElementById(roomId));
    }
}
