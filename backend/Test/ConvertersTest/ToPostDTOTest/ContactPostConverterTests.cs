using Xunit;
using DTOs.WithoutId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class ContactPostConverterTests
    {
        private readonly ContactPostConverter _converter;

        public ContactPostConverterTests()
        {
            _converter = new ContactPostConverter();
        }

        [Fact]
        public void Convert_ContactWithReservations_ReturnsContactPostDTO()
        {
            // Arrange
            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "123-456-7890",
                Email = "test@example.com"
            };

            var reservations = new List<Reservation>
            {
                new Reservation { ContactID = contact.ContactID, RoomID = Guid.NewGuid() },
                new Reservation { ContactID = contact.ContactID, RoomID = Guid.NewGuid() },
                new Reservation { ContactID = Guid.NewGuid(), RoomID = Guid.NewGuid() } // No debe aparecer en la lista
            };

            // Act
            var result = _converter.Convert(contact, reservations);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
            Assert.Equal(2, result.ReservationList.Count); // Verifica que las reservas relacionadas estén en la lista
        }

        [Fact]
        public void Convert_ContactWithNoReservations_ReturnsContactPostDTOWithEmptyList()
        {
            // Arrange
            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "987-654-3210",
                Email = "test2@example.com"
            };

            var reservations = new List<Reservation>();

            // Act
            var result = _converter.Convert(contact, reservations);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
            Assert.Empty(result.ReservationList); 
        }
    }
}
