using backend.Services;
using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest;

public class BathRoomServiceTests
{
    private readonly Mock<IDAO<Bathroom>> _mockBathroomDAO;
    private readonly BathRoomService _bathRoomService;

    public BathRoomServiceTests()
    {
        _mockBathroomDAO = new Mock<IDAO<Bathroom>>();
        _bathRoomService = new BathRoomService(_mockBathroomDAO.Object);
    }

    [Fact]
    public async Task GetAllElements_Returns_ListOfBathroomInfoDTOs()
    {
        // Arrange
        var bathrooms = new List<Bathroom>
        {
            new Bathroom { BathRoomID = Guid.NewGuid(), Shower = true, Toilet = true, DressingTable = true },
            new Bathroom { BathRoomID = Guid.NewGuid(), Shower = false, Toilet = true, DressingTable = false }
        };

        _mockBathroomDAO.Setup(x => x.ReadAll()).Returns(bathrooms);

        // Act
        var result = await _bathRoomService.GetAllElements();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(bathrooms[0].Shower, result[0].Shower);
        Assert.Equal(bathrooms[1].Toilet, result[1].Toilet);
    }

    [Fact]
    public async Task GetElementById_Returns_BathroomPostDTO_When_BathroomExists()
    {
        // Arrange
        var bathroomId = Guid.NewGuid();
        var bathroom = new Bathroom
        {
            BathRoomID = bathroomId,
            Shower = true,
            Toilet = true,
            DressingTable = false
        };

        _mockBathroomDAO.Setup(x => x.Read(bathroomId)).Returns(bathroom);

        // Act
        var result = await _bathRoomService.GetElementById(bathroomId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bathroom.Shower, result.Shower);
        Assert.Equal(bathroom.Toilet, result.Toilet);
        Assert.Equal(bathroom.DressingTable, result.DressingTable);
    }

    [Fact]
    public async Task GetElementById_ThrowsException_When_BathroomNotFound()
    {
        // Arrange
        var bathroomId = Guid.NewGuid();
        _mockBathroomDAO.Setup(x => x.Read(bathroomId)).Returns((Bathroom)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bathRoomService.GetElementById(bathroomId));
    }

    [Fact]
    public async Task CreateSingleElement_CreatesAndReturns_BathroomPostDTO()
    {
        // Arrange
        var bathroomPostDto = new BathroomPostDTO
        {
            Shower = true,
            Toilet = false,
            DressingTable = true
        };

        _mockBathroomDAO.Setup(x => x.Create(It.IsAny<Bathroom>())).Verifiable();

        // Act
        var result = await _bathRoomService.CreateSingleElement(bathroomPostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bathroomPostDto.Shower, result.Shower);
        Assert.Equal(bathroomPostDto.Toilet, result.Toilet);
        _mockBathroomDAO.Verify(x => x.Create(It.IsAny<Bathroom>()), Times.Once);
    }

    [Fact]
    public async Task CreateSingleElement_ThrowsException_When_BathroomPostDTOIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bathRoomService.CreateSingleElement(null));
    }

    [Fact]
    public async Task UpdateElementById_UpdatesAndReturns_BathroomPostDTO()
    {
        // Arrange
        var bathroomId = Guid.NewGuid();
        var bathroomDto = new BathroomPostDTO
        {
            Shower = true,
            Toilet = false,
            DressingTable = true
        };

        var bathroom = new Bathroom
        {
            BathRoomID = bathroomId,
            Shower = bathroomDto.Shower,
            Toilet = bathroomDto.Toilet,
            DressingTable = bathroomDto.DressingTable
        };

        _mockBathroomDAO.Setup(x => x.Update(It.IsAny<Bathroom>())).Verifiable();

        // Act
        var result = await _bathRoomService.UpdateElementById(bathroomId, bathroomDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bathroomDto.Shower, result.Shower);
        Assert.Equal(bathroomDto.Toilet, result.Toilet);
        _mockBathroomDAO.Verify(x => x.Update(It.IsAny<Bathroom>()), Times.Once);
    }

    [Fact]
    public async Task DeleteElementById_Returns_True_When_DeletedSuccessfully()
    {
        // Arrange
        var bathroomId = Guid.NewGuid();
        _mockBathroomDAO.Setup(x => x.Delete(bathroomId)).Returns(true);

        // Act
        var result = await _bathRoomService.DeleteElementById(bathroomId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteElementById_Returns_False_When_DeleteFails()
    {
        // Arrange
        var bathroomId = Guid.NewGuid();
        _mockBathroomDAO.Setup(x => x.Delete(bathroomId)).Returns(false);

        // Act
        var result = await _bathRoomService.DeleteElementById(bathroomId);

        // Assert
        Assert.False(result);
    }
}
