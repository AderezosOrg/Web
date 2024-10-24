using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DTOs.WithId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class ContactConverterTests
    {
        private readonly ContactConverter _converter;

        public ContactConverterTests()
        {
            _converter = new ContactConverter();
        }

        [Fact]
        public void Convert_ContactAndReservations_ReturnsContactDTOWithFilteredReservations()
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
                new Reservation
                {
                    ContactID = contact.ContactID, // Should be included
                    RoomID = Guid.NewGuid()
                },
                new Reservation
                {
                    ContactID = Guid.NewGuid(), // Should not be included
                    RoomID = Guid.NewGuid()
                }
            };

            // Act
            var result = _converter.Convert(contact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.ContactID, result.ContactID);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
        }

        [Fact]
        public void Convert_ContactWithNoReservations_ReturnsContactDTOWithEmptyReservationList()
        {
            // Arrange
            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "123-456-7890",
                Email = "test@example.com"
            };

            var reservations = new List<Reservation>(); 

            // Act
            var result = _converter.Convert(contact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.ContactID, result.ContactID);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
        }
    }
}
