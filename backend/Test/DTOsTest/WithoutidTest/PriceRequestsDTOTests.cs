using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class PriceRequestsDTOTests
{
    [Fact]
        public void PriceRequestsDTO_Should_Set_And_Get_Reservations()
        {
            // Arrange
            var priceRequestsDto = new PriceRequestsDTO();
            var reservations = new List<ReservationPostDTO>
            {
                new ReservationPostDTO { ContactId = Guid.NewGuid(), RoomId = Guid.NewGuid() },
                new ReservationPostDTO { ContactId = Guid.NewGuid(), RoomId = Guid.NewGuid() }
            };

            // Act
            priceRequestsDto.Reservations = reservations;

            // Assert
            Assert.Equal(reservations, priceRequestsDto.Reservations);
        }

        [Fact]
        public void PriceRequestsDTO_Should_Allow_Empty_Reservations_List()
        {
            // Arrange
            var priceRequestsDto = new PriceRequestsDTO();
            var reservations = new List<ReservationPostDTO>();

            // Act
            priceRequestsDto.Reservations = reservations;

            // Assert
            Assert.NotNull(priceRequestsDto.Reservations);
            Assert.Empty(priceRequestsDto.Reservations);
        }

        [Fact]
        public void PriceRequestsDTO_Reservations_Should_Be_Null_By_Default()
        {
            // Arrange
            var priceRequestsDto = new PriceRequestsDTO();

            // Assert
            Assert.Null(priceRequestsDto.Reservations);
        }

        [Fact]
        public void PriceRequestsDTO_Should_Handle_Null_Reservations_List()
        {
            // Arrange
            var priceRequestsDto = new PriceRequestsDTO();

            // Act
            priceRequestsDto.Reservations = null;

            // Assert
            Assert.Null(priceRequestsDto.Reservations);
        }

        [Fact]
        public void PriceRequestsDTO_Should_Contain_Reservations_List_Of_Correct_Size()
        {
            // Arrange
            var priceRequestsDto = new PriceRequestsDTO();
            var reservations = new List<ReservationPostDTO>
            {
                new ReservationPostDTO { ContactId = Guid.NewGuid(), RoomId = Guid.NewGuid() },
                new ReservationPostDTO { ContactId = Guid.NewGuid(), RoomId = Guid.NewGuid() },
                new ReservationPostDTO { ContactId = Guid.NewGuid(), RoomId = Guid.NewGuid() }
            };

            // Act
            priceRequestsDto.Reservations = reservations;

            // Assert
            Assert.Equal(3, priceRequestsDto.Reservations.Count);
        }
}