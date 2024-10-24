using Xunit;
using DTOs.WithId;
using Entities;
using Converters.ToDTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class UserConverterTests
    {
        private readonly UserConverter _converter;

        public UserConverterTests()
        {
            _converter = new UserConverter();
        }

        [Fact]
        public void Convert_UserContactAndHotels_ReturnsUserDTO()
        {
            // Arrange
            var user = new User
            {
                UserID = Guid.NewGuid(),
                Name = "John Doe",
                CINumber = "123456789"
            };

            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "123-456-7890",
                Email = "johndoe@example.com"
            };

            var hotels = new List<Hotel>
            {
                new Hotel { HotelID = Guid.NewGuid(), ContactID = contact.ContactID },
                new Hotel { HotelID = Guid.NewGuid(), ContactID = Guid.NewGuid() } // Different ContactID
            };

            // Act
            var result = _converter.Convert(user, contact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.CINumber, result.CINumber);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
        }

        [Fact]
        public void Convert_UserContactWithNoHotels_ReturnsUserDTOWithEmptyHotelList()
        {
            // Arrange
            var user = new User
            {
                UserID = Guid.NewGuid(),
                Name = "Jane Doe",
                CINumber = "987654321"
            };

            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "098-765-4321",
                Email = "janedoe@example.com"
            };

            var hotels = new List<Hotel>(); 

            // Act
            var result = _converter.Convert(user, contact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.CINumber, result.CINumber);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
        }
    }
}
