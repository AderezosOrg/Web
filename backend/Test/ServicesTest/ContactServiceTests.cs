using backend.Services;
using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;
namespace backend.Test.ServicesTest;
public class ContactServiceTests
{
    private readonly Mock<IDAO<Contact>> _mockContactDAO;
    private readonly Mock<IReservationDAO> _mockReservationDAO;
    private readonly ContactService _contactService;

    public ContactServiceTests()
    {
        _mockContactDAO = new Mock<IDAO<Contact>>();
        _mockReservationDAO = new Mock<IReservationDAO>();
        _contactService = new ContactService(_mockContactDAO.Object, _mockReservationDAO.Object);
    }

    [Fact]
    public async Task GetElementById_Returns_ContactPostDTO_When_ContactExists()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var contact = new Contact
        {
            ContactID = contactId,
            Email = "test@example.com",
            PhoneNumber = "1234567890"
        };

        _mockContactDAO.Setup(x => x.Read(contactId)).Returns(contact);
        _mockReservationDAO.Setup(x => x.GetReservationsByContactId(contactId)).Returns(new List<Reservation>());

        // Act
        var result = await _contactService.GetElementById(contactId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contact.Email, result.Email);
        Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
    }

    [Fact]
    public async Task GetElementById_ThrowsException_When_ContactNotFound()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        _mockContactDAO.Setup(x => x.Read(contactId)).Returns((Contact)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _contactService.GetElementById(contactId));
    }

    [Fact]
    public async Task GetAllElements_Returns_ListOfContactDTOs()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { ContactID = Guid.NewGuid(), Email = "contact1@example.com", PhoneNumber = "1234567890" },
            new Contact { ContactID = Guid.NewGuid(), Email = "contact2@example.com", PhoneNumber = "0987654321" }
        };

        _mockContactDAO.Setup(x => x.ReadAll()).Returns(contacts);
        _mockReservationDAO.Setup(x => x.GetReservationsByContactId(It.IsAny<Guid>())).Returns(new List<Reservation>());

        // Act
        var result = await _contactService.GetAllElements();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(contacts[0].Email, result[0].Email);
        Assert.Equal(contacts[1].PhoneNumber, result[1].PhoneNumber);
    }

    [Fact]
    public async Task CreateSingleElement_CreatesAndReturns_ContactDTO()
    {
        // Arrange
        var contactPostDto = new ContactPostDTO
        {
            Email = "newcontact@example.com",
            PhoneNumber = "1234567890"
        };

        var newContact = new Contact
        {
            ContactID = Guid.NewGuid(),
            Email = contactPostDto.Email,
            PhoneNumber = contactPostDto.PhoneNumber
        };

        _mockContactDAO.Setup(x => x.Create(It.IsAny<Contact>())).Verifiable();

        // Act
        var result = await _contactService.CreateSingleElement(contactPostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contactPostDto.Email, result.Email);
        Assert.Equal(contactPostDto.PhoneNumber, result.PhoneNumber);
        _mockContactDAO.Verify(x => x.Create(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task CreateSingleElement_ThrowsException_When_ContactPostDTOIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _contactService.CreateSingleElement(null));
    }

    [Fact]
    public async Task UpdateElementById_UpdatesAndReturns_ContactDTO()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var contactPostDto = new ContactPostDTO
        {
            Email = "updatedcontact@example.com",
            PhoneNumber = "9876543210"
        };

        var contact = new Contact
        {
            ContactID = contactId,
            Email = contactPostDto.Email,
            PhoneNumber = contactPostDto.PhoneNumber
        };

        _mockContactDAO.Setup(x => x.Update(It.IsAny<Contact>())).Verifiable();

        // Act
        var result = await _contactService.UpdateElementById(contactId, contactPostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contactPostDto.Email, result.Email);
        Assert.Equal(contactPostDto.PhoneNumber, result.PhoneNumber);
        _mockContactDAO.Verify(x => x.Update(It.IsAny<Contact>()), Times.Once);
    }
}
