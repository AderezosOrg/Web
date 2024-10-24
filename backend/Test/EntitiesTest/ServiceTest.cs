using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class ServiceTest
    {
        [Fact]
        public void Service_CanSet_ServiceID()
        {
            // Arrange
            var service = new Service();
            var serviceId = Guid.NewGuid();

            // Act
            service.ServiceID = serviceId;

            // Assert
            Assert.Equal(serviceId, service.ServiceID);
        }

        [Fact]
        public void Service_CanGet_ServiceID()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var service = new Service { ServiceID = serviceId };

            // Act & Assert
            Assert.Equal(serviceId, service.ServiceID);
        }

        [Fact]
        public void Service_CanSet_Type()
        {
            // Arrange
            var service = new Service();
            var serviceType = "Room Cleaning";

            // Act
            service.Type = serviceType;

            // Assert
            Assert.Equal(serviceType, service.Type);
        }

        [Fact]
        public void Service_CanGet_Type()
        {
            // Arrange
            var serviceType = "Room Cleaning";
            var service = new Service { Type = serviceType };

            // Act & Assert
            Assert.Equal(serviceType, service.Type);
        }

        [Fact]
        public void Service_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var service = new Service();

            // Assert
            Assert.Equal(Guid.Empty, service.ServiceID);
            Assert.Null(service.Type);
        }
    }
}