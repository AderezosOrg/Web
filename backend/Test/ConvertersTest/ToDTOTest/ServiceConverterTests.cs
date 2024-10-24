using Xunit;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class ServiceConverterTests
    {
        private readonly ServiceConverter _converter;

        public ServiceConverterTests()
        {
            _converter = new ServiceConverter();
        }

        [Fact]
        public void Convert_ServiceEntity_ReturnsServiceDTO()
        {
            // Arrange
            var service = new Service
            {
                ServiceID = Guid.NewGuid(),
                Type = "Cleaning"
            };

            // Act
            var result = _converter.Convert(service);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(service.ServiceID, result.ServiceID);
            Assert.Equal(service.Type, result.Type);
        }

        [Fact]
        public void Convert_ServicePostDTO_ReturnsServiceDTO()
        {
            // Arrange
            var postDto = new ServicePostDTO
            {
                Type = "Laundry"
            };

            var serviceId = Guid.NewGuid();

            // Act
            var result = _converter.Convert(postDto, serviceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(serviceId, result.ServiceID);
            Assert.Equal(postDto.Type, result.Type);
        }
    }
}