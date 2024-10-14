using Xunit;
using DTOs.WithId;
using System;
using System.Collections.Generic;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class ContactDTOTest
    {
        [Fact]
        public void ContactDTO_CanSet_ContactID()
        {
            // Arrange
            var contactDTO = new ContactDTO();
            var contactId = Guid.NewGuid();

            // Act
            contactDTO.ContactID = contactId;

            // Assert
            Assert.Equal(contactId, contactDTO.ContactID);
        }

        [Fact]
        public void ContactDTO_CanGet_ContactID()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var contactDTO = new ContactDTO { ContactID = contactId };

            // Act & Assert
            Assert.Equal(contactId, contactDTO.ContactID);
        }

        [Fact]
        public void ContactDTO_CanSet_PhoneNumber()
        {
            // Arrange
            var contactDTO = new ContactDTO();
            var phoneNumber = "123-456-7890";

            // Act
            contactDTO.PhoneNumber = phoneNumber;

            // Assert
            Assert.Equal(phoneNumber, contactDTO.PhoneNumber);
        }

        [Fact]
        public void ContactDTO_CanGet_PhoneNumber()
        {
            // Arrange
            var phoneNumber = "987-654-3210";
            var contactDTO = new ContactDTO { PhoneNumber = phoneNumber };

            // Act & Assert
            Assert.Equal(phoneNumber, contactDTO.PhoneNumber);
        }

        [Fact]
        public void ContactDTO_CanSet_Email()
        {
            // Arrange
            var contactDTO = new ContactDTO();
            var email = "test@example.com";

            // Act
            contactDTO.Email = email;

            // Assert
            Assert.Equal(email, contactDTO.Email);
        }

        [Fact]
        public void ContactDTO_CanGet_Email()
        {
            // Arrange
            var email = "user@example.com";
            var contactDTO = new ContactDTO { Email = email };

            // Act & Assert
            Assert.Equal(email, contactDTO.Email);
        }

        [Fact]
        public void ContactDTO_CanSet_ReservationList()
        {
            // Arrange
            var contactDTO = new ContactDTO();
            var reservations = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            contactDTO.ReservationList = reservations;

            // Assert
            Assert.Equal(reservations, contactDTO.ReservationList);
        }

        [Fact]
        public void ContactDTO_CanGet_ReservationList()
        {
            // Arrange
            var reservations = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var contactDTO = new ContactDTO { ReservationList = reservations };

            // Act & Assert
            Assert.Equal(reservations, contactDTO.ReservationList);
        }

        [Fact]
        public void ContactDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var contactDTO = new ContactDTO();

            // Assert
            Assert.Equal(Guid.Empty, contactDTO.ContactID);
            Assert.Null(contactDTO.PhoneNumber);
            Assert.Null(contactDTO.Email);
            Assert.Null(contactDTO.ReservationList);
        }

        [Fact]
        public void ContactDTO_ReservationList_IsInitialized()
        {
            // Arrange
            var contactDTO = new ContactDTO();

            // Act
            contactDTO.ReservationList = new List<Guid>();

            // Assert
            Assert.NotNull(contactDTO.ReservationList);
            Assert.Empty(contactDTO.ReservationList);
        }
    }
}
