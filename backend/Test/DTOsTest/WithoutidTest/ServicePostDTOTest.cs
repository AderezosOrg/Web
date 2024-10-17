using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class ServicePostDTOTest
    {
        [Fact]
        public void ServicePostDTO_CanSet_Type()
        {
            // Arrange
            var servicePostDTO = new ServicePostDTO();
            string type = "Room Service";

            // Act
            servicePostDTO.Type = type;

            // Assert
            Assert.Equal(type, servicePostDTO.Type);
        }

        [Fact]
        public void ServicePostDTO_CanGet_Type()
        {
            // Arrange
            var servicePostDTO = new ServicePostDTO { Type = "Laundry" };

            // Act & Assert
            Assert.Equal("Laundry", servicePostDTO.Type);
        }

        [Fact]
        public void ServicePostDTO_DefaultValues_AreNull()
        {
            // Arrange & Act
            var servicePostDTO = new ServicePostDTO();

            // Assert
            Assert.Null(servicePostDTO.Type);
        }
    }
}