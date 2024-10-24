using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class AvailabilityRequestDTOTests
{
    [Fact]
        public void AvailabilityRequestDTO_Should_Set_And_Get_StartDate()
        {
            // Arrange
            var availabilityRequestDto = new AvailabilityRequestDTO();
            var startDate = DateTime.Now;

            // Act
            availabilityRequestDto.StartDate = startDate;

            // Assert
            Assert.Equal(startDate, availabilityRequestDto.StartDate);
        }

        [Fact]
        public void AvailabilityRequestDTO_Should_Set_And_Get_EndDate()
        {
            // Arrange
            var availabilityRequestDto = new AvailabilityRequestDTO();
            var endDate = DateTime.Now.AddDays(1);

            // Act
            availabilityRequestDto.EndDate = endDate;

            // Assert
            Assert.Equal(endDate, availabilityRequestDto.EndDate);
        }

        [Fact]
        public void AvailabilityRequestDTO_Should_Set_And_Get_Capacity()
        {
            // Arrange
            var availabilityRequestDto = new AvailabilityRequestDTO();
            var capacity = 5;

            // Act
            availabilityRequestDto.Capacity = capacity;

            // Assert
            Assert.Equal(capacity, availabilityRequestDto.Capacity);
        }

        [Fact]
        public void AvailabilityRequestDTO_Should_Have_Default_Values()
        {
            // Arrange
            var availabilityRequestDto = new AvailabilityRequestDTO();

            // Assert
            Assert.Equal(default(DateTime), availabilityRequestDto.StartDate);
            Assert.Equal(default(DateTime), availabilityRequestDto.EndDate);
            Assert.Equal(0, availabilityRequestDto.Capacity);
        }

        [Fact]
        public void AvailabilityRequestDTO_StartDate_Should_Be_Before_EndDate()
        {
            // Arrange
            var availabilityRequestDto = new AvailabilityRequestDTO
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Capacity = 3
            };

            // Act & Assert
            Assert.True(availabilityRequestDto.StartDate < availabilityRequestDto.EndDate);
        }
}
