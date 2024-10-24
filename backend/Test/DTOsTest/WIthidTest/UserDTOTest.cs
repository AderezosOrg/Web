using Xunit;
using DTOs.WithId;
using System;
using System.Collections.Generic;

namespace backend.Test.DTOsTest.WithIdTest
{
    public class UserDTOTest
    {
        [Fact]
        public void UserDTO_CanSet_UserID()
        {
            // Arrange
            var userDTO = new UserDTO();
            var userId = Guid.NewGuid();

            // Act
            userDTO.UserID = userId;

            // Assert
            Assert.Equal(userId, userDTO.UserID);
        }

        [Fact]
        public void UserDTO_CanGet_UserID()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDTO = new UserDTO { UserID = userId };

            // Act & Assert
            Assert.Equal(userId, userDTO.UserID);
        }

        [Fact]
        public void UserDTO_CanSet_Name()
        {
            // Arrange
            var userDTO = new UserDTO();
            var name = "John Doe";

            // Act
            userDTO.Name = name;

            // Assert
            Assert.Equal(name, userDTO.Name);
        }

        [Fact]
        public void UserDTO_CanGet_Name()
        {
            // Arrange
            var name = "John Doe";
            var userDTO = new UserDTO { Name = name };

            // Act & Assert
            Assert.Equal(name, userDTO.Name);
        }

        [Fact]
        public void UserDTO_CanSet_CINumber()
        {
            // Arrange
            var userDTO = new UserDTO();
            var ciNumber = "12345678";

            // Act
            userDTO.CINumber = ciNumber;

            // Assert
            Assert.Equal(ciNumber, userDTO.CINumber);
        }

        [Fact]
        public void UserDTO_CanGet_CINumber()
        {
            // Arrange
            var ciNumber = "12345678";
            var userDTO = new UserDTO { CINumber = ciNumber };

            // Act & Assert
            Assert.Equal(ciNumber, userDTO.CINumber);
        }

        [Fact]
        public void UserDTO_CanSet_PhoneNumber()
        {
            // Arrange
            var userDTO = new UserDTO();
            var phoneNumber = "555-1234";

            // Act
            userDTO.PhoneNumber = phoneNumber;

            // Assert
            Assert.Equal(phoneNumber, userDTO.PhoneNumber);
        }

        [Fact]
        public void UserDTO_CanGet_PhoneNumber()
        {
            // Arrange
            var phoneNumber = "555-1234";
            var userDTO = new UserDTO { PhoneNumber = phoneNumber };

            // Act & Assert
            Assert.Equal(phoneNumber, userDTO.PhoneNumber);
        }

        [Fact]
        public void UserDTO_CanSet_Email()
        {
            // Arrange
            var userDTO = new UserDTO();
            var email = "johndoe@example.com";

            // Act
            userDTO.Email = email;

            // Assert
            Assert.Equal(email, userDTO.Email);
        }

        [Fact]
        public void UserDTO_CanGet_Email()
        {
            // Arrange
            var email = "johndoe@example.com";
            var userDTO = new UserDTO { Email = email };

            // Act & Assert
            Assert.Equal(email, userDTO.Email);
        }

        [Fact]
        public void UserDTO_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var userDTO = new UserDTO();

            // Assert
            Assert.Equal(Guid.Empty, userDTO.UserID);
            Assert.Null(userDTO.Name);
            Assert.Null(userDTO.CINumber);
            Assert.Null(userDTO.PhoneNumber);
            Assert.Null(userDTO.Email);
        }
    }
}
