using backend.Services;

using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;
public class PriceServiceTests
{
    private readonly Mock<IDAO<Hotel>> _mockHotelDAO;
    private readonly Mock<IDAO<Room>> _mockRoomDAO;
    private readonly PriceService _priceService;

    public PriceServiceTests()
    {
        _mockHotelDAO = new Mock<IDAO<Hotel>>();
        _mockRoomDAO = new Mock<IDAO<Room>>();
        _priceService = new PriceService(_mockRoomDAO.Object, _mockHotelDAO.Object);
    }

    [Fact]
    public async Task GetReservationPrice_Returns_CorrectTotalPrice()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();
        var room = new Room { RoomID = roomId, PricePerNight = 100, HotelID = hotelId };
        var hotel = new Hotel { HotelID = hotelId, Tax = 10 };

        var reservation = new ReservationPostDTO
        {
            RoomId = roomId,
            ReservationDate = DateTime.Now,
            UseDate = DateTime.Now.AddDays(3)
        };
        var reservations = new PriceRequestsDTO
        {
            Reservations = new List<ReservationPostDTO> { reservation }
        };

        _mockRoomDAO.Setup(x => x.Read(roomId)).Returns(room);
        _mockHotelDAO.Setup(x => x.Read(hotelId)).Returns(hotel);

        // Act
        var result = await _priceService.GetReservationPrice(reservations);

        // Assert
        Assert.Equal(330, result); // 110 (taxed price) * 3 nights
    }

    [Fact]
    public async Task GetReservationPartialPrice_Returns_CorrectPartialPrice()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var room = new Room { RoomID = roomId, PricePerNight = 100 };
        
        var reservation1 = new ReservationPostDTO
        {
            RoomId = roomId,
            ReservationDate = DateTime.Now,
            UseDate = DateTime.Now.AddDays(2)
        };
        var reservation2 = new ReservationPostDTO
        {
            RoomId = roomId,
            ReservationDate = DateTime.Now,
            UseDate = DateTime.Now.AddDays(3)
        };
        var reservations = new PriceRequestsDTO
        {
            Reservations = new List<ReservationPostDTO> { reservation1, reservation2 }
        };

        _mockRoomDAO.Setup(x => x.Read(roomId)).Returns(room);

        // Act
        var result = await _priceService.GetReservationPartialPrice(reservations);

        // Assert
        Assert.Equal(500, result); // 100 * 2 days + 100 * 3 days
    }

    [Fact]
    public async Task GetReservationTaxPrice_Returns_CorrectTaxPrice()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();
        var room = new Room { RoomID = roomId, PricePerNight = 100, HotelID = hotelId };
        var hotel = new Hotel { HotelID = hotelId, Tax = 10 };

        var reservation = new ReservationPostDTO
        {
            RoomId = roomId,
            ReservationDate = DateTime.Now,
            UseDate = DateTime.Now.AddDays(3)
        };
        var reservations = new PriceRequestsDTO
        {
            Reservations = new List<ReservationPostDTO> { reservation }
        };

        _mockRoomDAO.Setup(x => x.Read(roomId)).Returns(room);
        _mockHotelDAO.Setup(x => x.Read(hotelId)).Returns(hotel);

        // Act
        var result = await _priceService.GetReservationTaxPrice(reservations);

        // Assert
        Assert.Equal(30, result); // (100 * 0.1) * 3 nights
    }

    [Fact]
    public async Task GetReservationPriceByANight_Returns_CorrectTaxedPricePerNight()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();
        var room = new Room { RoomID = roomId, PricePerNight = 100, HotelID = hotelId };
        var hotel = new Hotel { HotelID = hotelId, Tax = 10 };

        var reservation = new ReservationPostDTO
        {
            RoomId = roomId,
            ReservationDate = DateTime.Now,
            UseDate = DateTime.Now.AddDays(1)
        };
        var reservations = new PriceRequestsDTO
        {
            Reservations = new List<ReservationPostDTO> { reservation }
        };

        _mockRoomDAO.Setup(x => x.Read(roomId)).Returns(room);
        _mockHotelDAO.Setup(x => x.Read(hotelId)).Returns(hotel);

        // Act
        var result = await _priceService.GetReservationPriceByANight(reservations);

        // Assert
        Assert.Equal(110, result); // 100 + 10% tax
    }
}

