using backend.Services;
using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest
{
    public class HotelServiceTests
    {
        private readonly Mock<IDAO<Hotel>> _mockHotelDAO;
        private readonly Mock<IDAO<User>> _mockUserDAO;
        private readonly Mock<IDAO<Contact>> _mockContactDAO;
        private readonly Mock<IDAO<Bathroom>> _mockBathroomDAO;
        private readonly HotelService _hotelService;

        public HotelServiceTests()
        {
            _mockHotelDAO = new Mock<IDAO<Hotel>>();
            _mockUserDAO = new Mock<IDAO<User>>();
            _mockContactDAO = new Mock<IDAO<Contact>>();
            _mockBathroomDAO = new Mock<IDAO<Bathroom>>();
            _hotelService = new HotelService(_mockHotelDAO.Object, _mockUserDAO.Object, _mockContactDAO.Object, _mockBathroomDAO.Object);
        }

        [Fact]
        public async Task GetElementById_Returns_HotelPostDTO_When_HotelExists()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel { HotelID = hotelId, UserID = Guid.NewGuid(), ContactID = Guid.NewGuid(), BathRoomID = Guid.NewGuid(), Name = "Test Hotel" };
            var user = new User { UserID = hotel.UserID };
            var contact = new Contact { ContactID = hotel.ContactID };
            var bathroom = new Bathroom { BathRoomID = hotel.BathRoomID };

            _mockHotelDAO.Setup(x => x.Read(hotelId)).Returns(hotel);
            _mockUserDAO.Setup(x => x.Read(hotel.UserID)).Returns(user);
            _mockContactDAO.Setup(x => x.Read(hotel.ContactID)).Returns(contact);
            _mockBathroomDAO.Setup(x => x.Read(hotel.BathRoomID)).Returns(bathroom);

            // Act
            var result = await _hotelService.GetElementById(hotelId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotel.Name, result.Name);
            Assert.Equal(hotel.Address, result.Address);
        }

        [Fact]
        public async Task GetElementById_ThrowsException_When_HotelNotFound()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            _mockHotelDAO.Setup(x => x.Read(hotelId)).Returns((Hotel)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _hotelService.GetElementById(hotelId));
        }

        [Fact]
        public async Task GetAllElements_Returns_ListOfHotelDTOs()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new Hotel { HotelID = Guid.NewGuid(), UserID = Guid.NewGuid(), ContactID = Guid.NewGuid(), BathRoomID = Guid.NewGuid(), Name = "Hotel 1" },
                new Hotel { HotelID = Guid.NewGuid(), UserID = Guid.NewGuid(), ContactID = Guid.NewGuid(), BathRoomID = Guid.NewGuid(), Name = "Hotel 2" }
            };

            _mockHotelDAO.Setup(x => x.ReadAll()).Returns(hotels);
            _mockUserDAO.Setup(x => x.Read(It.IsAny<Guid>())).Returns(new User());
            _mockContactDAO.Setup(x => x.Read(It.IsAny<Guid>())).Returns(new Contact());
            _mockBathroomDAO.Setup(x => x.Read(It.IsAny<Guid>())).Returns(new Bathroom());

            // Act
            var result = await _hotelService.GetAllElements();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(hotels[0].Name, result[0].Name);
            Assert.Equal(hotels[1].Name, result[1].Name);
        }

        [Fact]
        public async Task CreateSingleElement_CreatesAndReturns_HotelPostDTO()
        {
            // Arrange
            var hotelPostDto = new HotelPostDTO
            {
                Address = "123 Main St",
                AllowsPets = true,
                Name = "New Hotel"
            };

            _mockHotelDAO.Setup(x => x.Create(It.IsAny<Hotel>())).Verifiable();

            // Act
            var result = await _hotelService.CreateSingleElement(hotelPostDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotelPostDto.Name, result.Name);
            Assert.Equal(hotelPostDto.Address, result.Address);
            _mockHotelDAO.Verify(x => x.Create(It.IsAny<Hotel>()), Times.Once);
        }

        [Fact]
        public async Task CreateSingleElement_ThrowsException_When_HotelPostDTOIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _hotelService.CreateSingleElement(null));
        }
    }
}
