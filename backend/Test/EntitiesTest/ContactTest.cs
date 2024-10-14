using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class ContactTest
    {
        [Fact]
        public void Contact_CanSet_ContactID()
        {
            // Arrange
            var contact = new Contact();
            var contactId = Guid.NewGuid();

            // Act
            contact.ContactID = contactId;

            // Assert
            Assert.Equal(contactId, contact.ContactID);
        }

        [Fact]
        public void Contact_CanGet_ContactID()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var contact = new Contact { ContactID = contactId };

            // Act & Assert
            Assert.Equal(contactId, contact.ContactID);
        }

        [Fact]
        public void Contact_CanSet_PhoneNumber()
        {
            // Arrange
            var contact = new Contact();
            var phoneNumber = "123456789";

            // Act
            contact.PhoneNumber = phoneNumber;

            // Assert
            Assert.Equal(phoneNumber, contact.PhoneNumber);
        }

        [Fact]
        public void Contact_CanGet_PhoneNumber()
        {
            // Arrange
            var phoneNumber = "987654321";
            var contact = new Contact { PhoneNumber = phoneNumber };

            // Act & Assert
            Assert.Equal(phoneNumber, contact.PhoneNumber);
        }

        [Fact]
        public void Contact_CanSet_Email()
        {
            // Arrange
            var contact = new Contact();
            var email = "example@test.com";

            // Act
            contact.Email = email;

            // Assert
            Assert.Equal(email, contact.Email);
        }

        [Fact]
        public void Contact_CanGet_Email()
        {
            // Arrange
            var email = "user@example.com";
            var contact = new Contact { Email = email };

            // Act & Assert
            Assert.Equal(email, contact.Email);
        }

        [Fact]
        public void Contact_DefaultValues_AreNullOrEmpty()
        {
            // Arrange & Act
            var contact = new Contact();

            // Assert
            Assert.Null(contact.PhoneNumber);
            Assert.Null(contact.Email);
        }
    }
}
