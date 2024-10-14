using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class ServiceDTOTest
    {
        [Fact]
        public void ServiceDTO_CanSet_ServiceID()
        {
            // Arrange
            var serviceDTO = new ServiceDTO();
            var serviceId = Guid.NewGuid();

            // Act
            serviceDTO.ServiceID = serviceId;

            // Assert
            Assert.Equal(serviceId, serviceDTO.ServiceID);
        }

        [Fact]
        public void ServiceDTO_CanGet_ServiceID()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var serviceDTO = new ServiceDTO { ServiceID = serviceId };

            // Act & Assert
            Assert.Equal(serviceId, serviceDTO.ServiceID);
        }

        [Fact]
        public void ServiceDTO_CanSet_Type()
        {
            // Arrange
            var serviceDTO = new ServiceDTO();
            var type = "Cleaning";

            // Act
            serviceDTO.Type = type;

            // Assert
            Assert.Equal(type, serviceDTO.Type);
        }

        [Fact]
        public void ServiceDTO_CanGet_Type()
        {
            // Arrange
            var type = "Cleaning";
            var serviceDTO = new ServiceDTO { Type = type };

            // Act & Assert
            Assert.Equal(type, serviceDTO.Type);
        }

        [Fact]
        public void ServiceDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var serviceDTO = new ServiceDTO();

            // Assert
            Assert.Equal(Guid.Empty, serviceDTO.ServiceID);
            Assert.Null(serviceDTO.Type);
        }
    }
}