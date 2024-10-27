using backend.Services;
using backend.DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;
using Db;

namespace backend.Test.ServicesTest;
public class ReservationServiceTests
{
    private readonly Mock<IReservationDAO> _mockReservationDao;
    private readonly Mock<IDAO<Contact>> _mockContactDao;
    private readonly Mock<IDAO<Room>> _mockRoomDao;
    private readonly ReservationService _reservationService;

    public ReservationServiceTests()
    {
        _mockReservationDao = new Mock<IReservationDAO>();
        _mockContactDao = new Mock<IDAO<Contact>>();
        _mockRoomDao = new Mock<IDAO<Room>>();
        _reservationService = new ReservationService(_mockReservationDao.Object, _mockContactDao.Object, _mockRoomDao.Object);
    }

    [Fact]
    public async Task GetReservationsByContactId_ReturnsReservations()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var reservation = new Reservation { ContactID = contactId, RoomID = roomId, Cancelled = false };
        _mockReservationDao.Setup(dao => dao.GetReservationsByContactId(contactId)).Returns(new List<Reservation> { reservation });
        _mockContactDao.Setup(dao => dao.Read(contactId)).Returns(new Contact { ContactID = contactId });
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns(new Room { RoomID = roomId });

        // Act
        var result = await _reservationService.GetReservationsByContactId(contactId);

        // Assert
        Assert.Single(result);
        Assert.Equal(contactId, result[0].ContactID);
        Assert.Equal(roomId, result[0].RoomID);
    }

    [Fact]
    public async Task GetReservationsByRoomId_ReturnsReservations()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var reservation = new Reservation { ContactID = contactId, RoomID = roomId, Cancelled = false };
        _mockReservationDao.Setup(dao => dao.GetReservationsByRoomId(roomId)).Returns(new List<Reservation> { reservation });
        _mockContactDao.Setup(dao => dao.Read(contactId)).Returns(new Contact { ContactID = contactId });
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns(new Room { RoomID = roomId });

        // Act
        var result = await _reservationService.GetReservationsByRoomId(roomId);

        // Assert
        Assert.Single(result);
        Assert.Equal(contactId, result[0].ContactID);
        Assert.Equal(roomId, result[0].RoomID);
    }

    [Fact]
    public async Task GetAllElements_ReturnsAllReservations()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var reservation = new Reservation { ContactID = contactId, RoomID = roomId, Cancelled = false };
        _mockReservationDao.Setup(dao => dao.ReadAll()).Returns(new List<Reservation> { reservation });
        _mockContactDao.Setup(dao => dao.Read(contactId)).Returns(new Contact { ContactID = contactId });
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns(new Room { RoomID = roomId });

        // Act
        var result = await _reservationService.GetAllElements();

        // Assert
        Assert.Single(result);
        Assert.Equal(contactId, result[0].ContactID);
        Assert.Equal(roomId, result[0].RoomID);
    }

    [Fact]
    public async Task CreateReservation_CreatesAndReturnsReservations()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var reservationPostDTO = new ReservationPostDTO { ContactId = contactId, RoomId = roomId, ReservationDate = DateTime.Now, UseDate = DateTime.Now.AddDays(1) };
        var contact = new Contact { ContactID = contactId };
        var room = new Room { RoomID = roomId };
        
        _mockContactDao.Setup(dao => dao.Read(contactId)).Returns(contact);
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns(room);

        // Act
        var result = await _reservationService.CreateReservation(new[] { reservationPostDTO });

        // Assert
        Assert.Single(result);
        Assert.Equal(contactId, result[0].ContactID);
        Assert.Equal(roomId, result[0].RoomID);
    }

    [Fact]
    public async Task CancelReservation_CancelsAndReturnsUpdatedReservation()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var reservation = new Reservation { ContactID = contactId, RoomID = roomId, Cancelled = false };
        var cancelDto = new CancelReservationDTO { ContactID = contactId, RoomID = roomId };
        _mockReservationDao.Setup(dao => dao.GetReservationsByContactId(contactId)).Returns(new List<Reservation> { reservation });
        _mockContactDao.Setup(dao => dao.Read(contactId)).Returns(new Contact { ContactID = contactId });
        _mockRoomDao.Setup(dao => dao.Read(roomId)).Returns(new Room { RoomID = roomId });

        // Act
        var result = await _reservationService.CancelReservation(cancelDto);

        // Assert
        Assert.False(result.Cancelled);
        Assert.Equal(contactId, result.ContactID);
        Assert.Equal(roomId, result.RoomID);
    }
}
