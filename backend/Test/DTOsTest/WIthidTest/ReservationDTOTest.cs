using Xunit;
using DTOs.WithId;
using System;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class ReservationDTOTest
    {
        [Fact]
        public void ReservationDTO_CanSet_ContactID()
        {
            // Arrange
            var reservationDTO = new ReservationDTO();
            var contactId = Guid.NewGuid();

            // Act
            reservationDTO.ContactID = contactId;

            // Assert
            Assert.Equal(contactId, reservationDTO.ContactID);
        }

        [Fact]
        public void ReservationDTO_CanGet_ContactID()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var reservationDTO = new ReservationDTO { ContactID = contactId };

            // Act & Assert
            Assert.Equal(contactId, reservationDTO.ContactID);
        }

        [Fact]
        public void ReservationDTO_CanSet_RoomID()
        {
            // Arrange
            var reservationDTO = new ReservationDTO();
            var roomId = Guid.NewGuid();

            // Act
            reservationDTO.RoomID = roomId;

            // Assert
            Assert.Equal(roomId, reservationDTO.RoomID);
        }

        [Fact]
        public void ReservationDTO_CanGet_RoomID()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var reservationDTO = new ReservationDTO { RoomID = roomId };

            // Act & Assert
            Assert.Equal(roomId, reservationDTO.RoomID);
        }

        [Fact]
        public void ReservationDTO_CanSet_ReservationDate()
        {
            // Arrange
            var reservationDTO = new ReservationDTO();
            var reservationDate = DateTime.Now;

            // Act
            reservationDTO.ReservationDate = reservationDate;

            // Assert
            Assert.Equal(reservationDate, reservationDTO.ReservationDate);
        }

        [Fact]
        public void ReservationDTO_CanGet_ReservationDate()
        {
            // Arrange
            var reservationDate = DateTime.Now;
            var reservationDTO = new ReservationDTO { ReservationDate = reservationDate };

            // Act & Assert
            Assert.Equal(reservationDate, reservationDTO.ReservationDate);
        }

        [Fact]
        public void ReservationDTO_CanSet_UseDate()
        {
            // Arrange
            var reservationDTO = new ReservationDTO();
            var useDate = DateTime.Now.AddDays(1);

            // Act
            reservationDTO.UseDate = useDate;

            // Assert
            Assert.Equal(useDate, reservationDTO.UseDate);
        }

        [Fact]
        public void ReservationDTO_CanGet_UseDate()
        {
            // Arrange
            var useDate = DateTime.Now.AddDays(1);
            var reservationDTO = new ReservationDTO { UseDate = useDate };

            // Act & Assert
            Assert.Equal(useDate, reservationDTO.UseDate);
        }

        [Fact]
        public void ReservationDTO_CanSet_Cancelled()
        {
            // Arrange
            var reservationDTO = new ReservationDTO();

            // Act
            reservationDTO.Cancelled = true;

            // Assert
            Assert.True(reservationDTO.Cancelled);
        }

        [Fact]
        public void ReservationDTO_CanGet_Cancelled()
        {
            // Arrange
            var reservationDTO = new ReservationDTO { Cancelled = false };

            // Act & Assert
            Assert.False(reservationDTO.Cancelled);
        }

        [Fact]
        public void ReservationDTO_CanSet_UserProperties()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                UserPhoneNumber = "123-456-7890",
                UserEmail = "user@example.com"
            };

            // Act & Assert
            Assert.Equal("123-456-7890", reservationDTO.UserPhoneNumber);
            Assert.Equal("user@example.com", reservationDTO.UserEmail);
        }

        [Fact]
        public void ReservationDTO_CanSet_RoomProperties()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                RoomCode = "R101",
                RoomFloorNumber = 2,
                PricePerNight = 150.00m
            };

            // Act & Assert
            Assert.Equal("R101", reservationDTO.RoomCode);
            Assert.Equal(2, reservationDTO.RoomFloorNumber);
            Assert.Equal(150.00m, reservationDTO.PricePerNight);
        }

        [Fact]
        public void ReservationDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var reservationDTO = new ReservationDTO();

            // Assert
            Assert.Equal(Guid.Empty, reservationDTO.ContactID);
            Assert.Equal(Guid.Empty, reservationDTO.RoomID);
            Assert.Equal(default(DateTime), reservationDTO.ReservationDate);
            Assert.Equal(default(DateTime), reservationDTO.UseDate);
            Assert.False(reservationDTO.Cancelled);
            Assert.Null(reservationDTO.UserPhoneNumber);
            Assert.Null(reservationDTO.UserEmail);
            Assert.Null(reservationDTO.RoomCode);
            Assert.Equal(0, reservationDTO.RoomFloorNumber);
            Assert.Equal(0m, reservationDTO.PricePerNight);
        }
    }
}
