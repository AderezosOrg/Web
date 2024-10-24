using Xunit;
using DTOs.WithoutId;
using System;
using System.Collections.Generic;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class ContactPostDTOTest
    {
        [Fact]
        public void ContactPostDTO_CanSet_PhoneNumber()
        {
            // Arrange
            var contactPostDTO = new ContactPostDTO();
            var phoneNumber = "123-456-7890";

            // Act
            contactPostDTO.PhoneNumber = phoneNumber;

            // Assert
            Assert.Equal(phoneNumber, contactPostDTO.PhoneNumber);
        }

        [Fact]
        public void ContactPostDTO_CanGet_PhoneNumber()
        {
            // Arrange
            var contactPostDTO = new ContactPostDTO { PhoneNumber = "987-654-3210" };

            // Act & Assert
            Assert.Equal("987-654-3210", contactPostDTO.PhoneNumber);
        }

        [Fact]
        public void ContactPostDTO_CanSet_Email()
        {
            // Arrange
            var contactPostDTO = new ContactPostDTO();
            var email = "test@example.com";

            // Act
            contactPostDTO.Email = email;

            // Assert
            Assert.Equal(email, contactPostDTO.Email);
        }

        [Fact]
        public void ContactPostDTO_CanGet_Email()
        {
            // Arrange
            var contactPostDTO = new ContactPostDTO { Email = "user@example.com" };

            // Act & Assert
            Assert.Equal("user@example.com", contactPostDTO.Email);
        }
        
        [Fact]
        public void ContactPostDTO_DefaultValues_AreNull()
        {
            // Arrange & Act
            var contactPostDTO = new ContactPostDTO();

            // Assert
            Assert.Null(contactPostDTO.PhoneNumber);
            Assert.Null(contactPostDTO.Email);
        }
    }
}
