using Xunit;
using DTOs.WithId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class ReservationConverterTests
    {
        private readonly ReservationConverter _converter;

        public ReservationConverterTests()
        {
            _converter = new ReservationConverter();
        }

        [Fact]
        public void Convert_ReservationContactRoom_ReturnsReservationDTO()
        {
            // Arrange
            var reservation = new Reservation
            {
                RoomID = Guid.NewGuid(),
                ReservationDate = DateTime.UtcNow,
                UseDate = DateTime.UtcNow.AddDays(1),
                ContactID = Guid.NewGuid()
            };

            var contact = new Contact
            {
                ContactID = reservation.ContactID,
                PhoneNumber = "123-456-7890",
                Email = "user@example.com"
            };

            var room = new Room
            {
                RoomID = reservation.RoomID,
                Code = "A101",
                FloorNumber = 1,
                PricePerNight = 150.00m
            };

            // Act
            var result = _converter.Convert(reservation, contact, room);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reservation.ContactID, result.ContactID);
            Assert.Equal(reservation.RoomID, result.RoomID);
            Assert.Equal(reservation.ReservationDate, result.ReservationDate);
            Assert.Equal(reservation.UseDate, result.UseDate);
            Assert.Equal(contact.PhoneNumber, result.UserPhoneNumber);
            Assert.Equal(contact.Email, result.UserEmail);
            Assert.Equal(room.Code, result.RoomCode);
            Assert.Equal(room.FloorNumber, result.RoomFloorNumber);
            Assert.Equal(room.PricePerNight, result.PricePerNight);
        }
    }
}