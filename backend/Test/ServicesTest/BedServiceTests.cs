using backend.Services;
using Db;
using DTOs.WithoutId;
using Entities;
using Moq;
using Xunit;

namespace backend.Test.ServicesTest
{
    public class BedServiceTests
    {
        private readonly Mock<IDAO<Bed>> _mockBedDAO;
        private readonly BedService _bedService;

        public BedServiceTests()
        {
            _mockBedDAO = new Mock<IDAO<Bed>>();
            _bedService = new BedService(_mockBedDAO.Object);
        }

        [Fact]
        public async Task GetAllElements_Returns_ListOfBedInfoDTOs()
        {
            // Arrange
            var beds = new List<Bed>
            {
                new Bed { BedID = Guid.NewGuid(), Capacity = 2, Size = "Queen" },
                new Bed { BedID = Guid.NewGuid(), Capacity = 1, Size = "Single" }
            };

            _mockBedDAO.Setup(x => x.ReadAll()).Returns(beds);

            // Act
            var result = await _bedService.GetAllElements();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(beds[0].Capacity, result[0].Capacity);
            Assert.Equal(beds[1].Size, result[1].Size);
        }

        [Fact]
        public async Task GetElementById_Returns_BedPostDTO_When_BedExists()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            var bed = new Bed
            {
                BedID = bedId,
                Capacity = 2,
                Size = "Queen"
            };

            _mockBedDAO.Setup(x => x.Read(bedId)).Returns(bed);

            // Act
            var result = await _bedService.GetElementById(bedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bed.Capacity, result.Capacity);
            Assert.Equal(bed.Size, result.Size);
        }

        [Fact]
        public async Task GetElementById_ThrowsException_When_BedNotFound()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            _mockBedDAO.Setup(x => x.Read(bedId)).Returns((Bed)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _bedService.GetElementById(bedId));
        }

        [Fact]
        public async Task CreateSingleElement_CreatesAndReturns_BedPostDTO()
        {
            // Arrange
            var bedPostDto = new BedPostDTO
            {
                Capacity = 2,
                Size = "King"
            };

            _mockBedDAO.Setup(x => x.Create(It.IsAny<Bed>())).Verifiable();

            // Act
            var result = await _bedService.CreateSingleElement(bedPostDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bedPostDto.Capacity, result.Capacity);
            Assert.Equal(bedPostDto.Size, result.Size);
            _mockBedDAO.Verify(x => x.Create(It.IsAny<Bed>()), Times.Once);
        }

        [Fact]
        public async Task CreateSingleElement_ThrowsException_When_BedPostDTOIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _bedService.CreateSingleElement(null));
        }

        [Fact]
        public async Task UpdateElementById_UpdatesAndReturns_BedPostDTO()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            var bedDto = new BedPostDTO
            {
                Capacity = 3,
                Size = "Full"
            };

            var bed = new Bed
            {
                BedID = bedId,
                Capacity = bedDto.Capacity,
                Size = bedDto.Size
            };

            _mockBedDAO.Setup(x => x.Update(It.IsAny<Bed>())).Verifiable();

            // Act
            var result = await _bedService.UpdateElementById(bedId, bedDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bedDto.Capacity, result.Capacity);
            Assert.Equal(bedDto.Size, result.Size);
            _mockBedDAO.Verify(x => x.Update(It.IsAny<Bed>()), Times.Once);
        }

        [Fact]
        public async Task DeleteElementById_Returns_True_When_DeletedSuccessfully()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            _mockBedDAO.Setup(x => x.Delete(bedId)).Returns(true);

            // Act
            var result = await _bedService.DeleteElementById(bedId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteElementById_Returns_False_When_DeleteFails()
        {
            // Arrange
            var bedId = Guid.NewGuid();
            _mockBedDAO.Setup(x => x.Delete(bedId)).Returns(false);

            // Act
            var result = await _bedService.DeleteElementById(bedId);

            // Assert
            Assert.False(result);
        }
    }
}