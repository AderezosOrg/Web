using Xunit;
using DTOs.WithId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class ServicePostConverterTests
    {
        private readonly ServicePostConverter _converter;

        public ServicePostConverterTests()
        {
            _converter = new ServicePostConverter();
        }

        [Fact]
        public void Convert_ValidService_ReturnsServicePostDTO()
        {
            // Arrange
            var service = new Service
            {
                ServiceID = Guid.NewGuid(),
                Type = "Room Cleaning"
            };

            // Act
            var result = _converter.Convert(service);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(service.Type, result.Type);
        }
    }
}