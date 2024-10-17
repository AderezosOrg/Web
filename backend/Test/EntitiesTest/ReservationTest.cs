using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class ReservationTest
    {
        [Fact]
        public void Reservation_CanSet_ContactID()
        {
            // Arrange
            var reservation = new Reservation();
            var contactId = Guid.NewGuid();

            // Act
            reservation.ContactID = contactId;

            // Assert
            Assert.Equal(contactId, reservation.ContactID);
        }

        [Fact]
        public void Reservation_CanGet_ContactID()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var reservation = new Reservation { ContactID = contactId };

            // Act & Assert
            Assert.Equal(contactId, reservation.ContactID);
        }

        [Fact]
        public void Reservation_CanSet_RoomID()
        {
            // Arrange
            var reservation = new Reservation();
            var roomId = Guid.NewGuid();

            // Act
            reservation.RoomID = roomId;

            // Assert
            Assert.Equal(roomId, reservation.RoomID);
        }

        [Fact]
        public void Reservation_CanGet_RoomID()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var reservation = new Reservation { RoomID = roomId };

            // Act & Assert
            Assert.Equal(roomId, reservation.RoomID);
        }

        [Fact]
        public void Reservation_CanSet_ReservationDate()
        {
            // Arrange
            var reservation = new Reservation();
            var reservationDate = DateTime.UtcNow;

            // Act
            reservation.ReservationDate = reservationDate;

            // Assert
            Assert.Equal(reservationDate, reservation.ReservationDate);
        }

        [Fact]
        public void Reservation_CanGet_ReservationDate()
        {
            // Arrange
            var reservationDate = DateTime.UtcNow;
            var reservation = new Reservation { ReservationDate = reservationDate };

            // Act & Assert
            Assert.Equal(reservationDate, reservation.ReservationDate);
        }

        [Fact]
        public void Reservation_CanSet_UseDate()
        {
            // Arrange
            var reservation = new Reservation();
            var useDate = DateTime.UtcNow.AddDays(1); 

            // Act
            reservation.UseDate = useDate;

            // Assert
            Assert.Equal(useDate, reservation.UseDate);
        }

        [Fact]
        public void Reservation_CanGet_UseDate()
        {
            // Arrange
            var useDate = DateTime.UtcNow.AddDays(1);
            var reservation = new Reservation { UseDate = useDate };

            // Act & Assert
            Assert.Equal(useDate, reservation.UseDate);
        }

        [Fact]
        public void Reservation_CanSet_Cancelled()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            reservation.Cancelled = true;

            // Assert
            Assert.True(reservation.Cancelled);
        }

        [Fact]
        public void Reservation_CanGet_Cancelled()
        {
            // Arrange
            var reservation = new Reservation { Cancelled = false };

            // Act & Assert
            Assert.False(reservation.Cancelled);
        }

        [Fact]
        public void Reservation_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var reservation = new Reservation();

            // Assert
            Assert.Equal(Guid.Empty, reservation.ContactID);
            Assert.Equal(Guid.Empty, reservation.RoomID);
            Assert.Equal(default(DateTime), reservation.ReservationDate);
            Assert.Equal(default(DateTime), reservation.UseDate);
            Assert.False(reservation.Cancelled);
        }
    }
}
