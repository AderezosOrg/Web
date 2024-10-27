using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

public class ServiceServiceTests
{
    private readonly Mock<IDAO<Service>> _serviceDaoMock;
    private readonly ServiceService _serviceService;

    public ServiceServiceTests()
    {
        _serviceDaoMock = new Mock<IDAO<Service>>();
        _serviceService = new ServiceService(_serviceDaoMock.Object);
    }

    [Fact]
    public async Task GetElementById_ValidId_ReturnsServicePostDTO()
    {
        // Arrange
        var serviceId = Guid.NewGuid();
        var service = new Service { ServiceID = serviceId, Type = "Cleaning" };
        _serviceDaoMock.Setup(x => x.Read(serviceId)).Returns(service);

        // Act
        var result = await _serviceService.GetElementById(serviceId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(service.Type, result.Type);
    }

    [Fact]
    public async Task GetElementById_InvalidId_ThrowsException()
    {
        // Arrange
        var serviceId = Guid.NewGuid();
        _serviceDaoMock.Setup(x => x.Read(serviceId)).Returns((Service)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _serviceService.GetElementById(serviceId));
    }

    [Fact]
    public async Task GetAllElements_ReturnsListOfServiceDTO()
    {
        // Arrange
        var services = new List<Service>
        {
            new Service { ServiceID = Guid.NewGuid(), Type = "Cleaning" },
            new Service { ServiceID = Guid.NewGuid(), Type = "Room Service" }
        };
        _serviceDaoMock.Setup(x => x.ReadAll()).Returns(services);

        // Act
        var result = await _serviceService.GetAllElements();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(services.Count, result.Count);
        Assert.Equal(services[0].ServiceID, result[0].ServiceID);
        Assert.Equal(services[0].Type, result[0].Type);
    }

    [Fact]
    public async Task CreateSingleElement_ValidServicePostDTO_ReturnsServicePostDTO()
    {
        // Arrange
        var servicePostDto = new ServicePostDTO { Type = "Cleaning" };

        // Act
        var result = await _serviceService.CreateSingleElement(servicePostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(servicePostDto.Type, result.Type);
        _serviceDaoMock.Verify(x => x.Create(It.IsAny<Service>()), Times.Once);
    }

    [Fact]
    public async Task CreateSingleElement_NullServicePostDTO_ThrowsException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _serviceService.CreateSingleElement(null));
    }

    [Fact]
    public async Task UpdateElementById_ValidIdAndServicePostDTO_ReturnsUpdatedServicePostDTO()
    {
        // Arrange
        var serviceId = Guid.NewGuid();
        var servicePostDto = new ServicePostDTO { Type = "Updated Type" };
        var service = new Service { ServiceID = serviceId, Type = "Updated Type" };

        _serviceDaoMock.Setup(x => x.Update(service)).Verifiable();

        // Act
        var result = await _serviceService.UpdateElementById(serviceId, servicePostDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(servicePostDto.Type, result.Type);
        _serviceDaoMock.Verify(x => x.Update(It.IsAny<Service>()), Times.Once);
    }
}
