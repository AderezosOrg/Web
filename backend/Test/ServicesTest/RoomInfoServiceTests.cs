using backend.Services;
using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using Db;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;

public class RoomInfoServiceTests
{
    private readonly Mock<IDAO<Room>> _roomDaoMock;
    private readonly Mock<IBedInformationDAO> _bedInformationDaoMock;
    private readonly Mock<IRoombathInformationDAO> _roomBathInformationDaoMock;
    private readonly Mock<IRoomServicesDAO> _roomServicesDaoMock;
    private readonly Mock<IDAO<Bathroom>> _bathroomDaoMock;
    private readonly Mock<IDAO<Bed>> _bedDaoMock;
    private readonly Mock<IDAO<Service>> _serviceDaoMock;
    private readonly Mock<IBedService> _bedServiceMock;
    private readonly Mock<IServiceService> _serviceServiceMock;
    private readonly BedConverter _bedConverter;
    private readonly BathroomConverter _bathroomConverter;
    private readonly ServiceConverter _serviceConverter;
    private readonly RoomInfoService _roomInfoService;

    public RoomInfoServiceTests()
    {
        _roomDaoMock = new Mock<IDAO<Room>>();
        _bedInformationDaoMock = new Mock<IBedInformationDAO>();
        _roomBathInformationDaoMock = new Mock<IRoombathInformationDAO>();
        _roomServicesDaoMock = new Mock<IRoomServicesDAO>();
        _bathroomDaoMock = new Mock<IDAO<Bathroom>>();
        _bedDaoMock = new Mock<IDAO<Bed>>();
        _serviceDaoMock = new Mock<IDAO<Service>>();
        _bedServiceMock = new Mock<IBedService>();
        _serviceServiceMock = new Mock<IServiceService>();
        _bedConverter = new BedConverter();
        _bathroomConverter = new BathroomConverter();
        _serviceConverter = new ServiceConverter();

        _roomInfoService = new RoomInfoService(
            _bedServiceMock.Object,
            _serviceServiceMock.Object,
            _roomDaoMock.Object,
            _bedInformationDaoMock.Object,
            _roomBathInformationDaoMock.Object,
            _roomServicesDaoMock.Object,
            _bathroomDaoMock.Object,
            _bedDaoMock.Object,
            _serviceDaoMock.Object
        );
    }
    
    [Fact]
    public async Task GetRoomBedsById_ValidRoomId_ReturnsListOfBedDTO()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var roomTemplateId = Guid.NewGuid();
        var bedId = Guid.NewGuid();
        var bedInformation = new BedInformation { BedID = bedId };
        var bed = new Bed { BedID = bedId };

        _roomDaoMock.Setup(x => x.Read(roomId)).Returns(new Room { RoomID = roomId, RoomTemplateID = roomTemplateId });
        _bedInformationDaoMock.Setup(x => x.GetBedInformationByRoomTemplateId(roomTemplateId)).Returns(new List<BedInformation> { bedInformation });
        _bedDaoMock.Setup(x => x.Read(bedId)).Returns(bed);
        _bedServiceMock.Setup(x => x.GetElementById(bedId)).ReturnsAsync(new BedPostDTO()
        {
            Capacity = 2,
            Size = "medium"
        });

        // Act
        var result = await _roomInfoService.GetRoomBedsById(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(bedId, result[0].BedID);
    }

    [Fact]
    public async Task GetRoomBedsById_InvalidRoomId_ThrowsException()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _roomDaoMock.Setup(x => x.Read(roomId)).Returns((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _roomInfoService.GetRoomBedsById(roomId));
    }
    [Fact]
    public async Task GetRoomBathroomsById_ValidRoomId_ReturnsListOfBathroomDTO()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var roomTemplateId = Guid.NewGuid();
        var bathRoomId = Guid.NewGuid();
        var bathInformation = new RoomBathInformation { BathRoomID = bathRoomId };
        var bathroom = new Bathroom { BathRoomID = bathRoomId };

        _roomDaoMock.Setup(x => x.Read(roomId)).Returns(new Room { RoomID = roomId, RoomTemplateID = roomTemplateId });
        _roomBathInformationDaoMock.Setup(x => x.GetRoombathInformationsByRoomTemplateId(roomTemplateId)).Returns(new List<RoomBathInformation> { bathInformation });
        _bathroomDaoMock.Setup(x => x.Read(bathRoomId)).Returns(bathroom);

        // Act
        var result = await _roomInfoService.GetRoomBathroomsById(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(bathRoomId, result[0].BathRoomID);
    }

    [Fact]
    public async Task GetRoomBathroomsById_InvalidRoomId_ThrowsException()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _roomDaoMock.Setup(x => x.Read(roomId)).Returns((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _roomInfoService.GetRoomBathroomsById(roomId));
    }
    
    [Fact]
    public async Task GetRoomServicesById_ValidRoomId_ReturnsListOfServiceDTO()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var serviceId = Guid.NewGuid();
        var roomServices = new RoomServices { ServiceID = serviceId };
        var service = new Service { ServiceID = serviceId };

        _roomDaoMock.Setup(x => x.Read(roomId)).Returns(new Room { RoomID = roomId });
        _roomServicesDaoMock.Setup(x => x.GetRoomServicesByRoomId(roomId)).Returns(new List<RoomServices> { roomServices });
        _serviceDaoMock.Setup(x => x.Read(serviceId)).Returns(service);
        _serviceServiceMock.Setup(x => x.GetElementById(serviceId)).ReturnsAsync(new ServicePostDTO() { Type = "petservice" });

        // Act
        var result = await _roomInfoService.GetRoomServicesById(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(serviceId, result[0].ServiceID);
    }

    [Fact]
    public async Task GetRoomServicesById_InvalidRoomId_ThrowsException()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _roomDaoMock.Setup(x => x.Read(roomId)).Returns((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _roomInfoService.GetRoomServicesById(roomId));
    }
}