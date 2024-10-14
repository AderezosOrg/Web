using System;
using System.Collections.Generic;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class UserPostDTOTest
    {
        [Fact]
        public void UserPostDTO_CanSet_Name()
        {
            // Arrange
            var userPostDTO = new UserPostDTO();
            string name = "John Doe";

            // Act
            userPostDTO.Name = name;

            // Assert
            Assert.Equal(name, userPostDTO.Name);
        }

        [Fact]
        public void UserPostDTO_CanGet_Name()
        {
            // Arrange
            var userPostDTO = new UserPostDTO { Name = "Jane Doe" };

            // Act & Assert
            Assert.Equal("Jane Doe", userPostDTO.Name);
        }

        [Fact]
        public void UserPostDTO_CanSet_CINumber()
        {
            // Arrange
            var userPostDTO = new UserPostDTO();
            string ciNumber = "123456789";

            // Act
            userPostDTO.CINumber = ciNumber;

            // Assert
            Assert.Equal(ciNumber, userPostDTO.CINumber);
        }

        [Fact]
        public void UserPostDTO_CanGet_CINumber()
        {
            // Arrange
            var userPostDTO = new UserPostDTO { CINumber = "987654321" };

            // Act & Assert
            Assert.Equal("987654321", userPostDTO.CINumber);
        }

        [Fact]
        public void UserPostDTO_CanSet_PhoneNumber()
        {
            // Arrange
            var userPostDTO = new UserPostDTO();
            string phoneNumber = "555-1234";

            // Act
            userPostDTO.PhoneNumber = phoneNumber;

            // Assert
            Assert.Equal(phoneNumber, userPostDTO.PhoneNumber);
        }

        [Fact]
        public void UserPostDTO_CanGet_PhoneNumber()
        {
            // Arrange
            var userPostDTO = new UserPostDTO { PhoneNumber = "555-5678" };

            // Act & Assert
            Assert.Equal("555-5678", userPostDTO.PhoneNumber);
        }

        [Fact]
        public void UserPostDTO_CanSet_Email()
        {
            // Arrange
            var userPostDTO = new UserPostDTO();
            string email = "user@example.com";

            // Act
            userPostDTO.Email = email;

            // Assert
            Assert.Equal(email, userPostDTO.Email);
        }

        [Fact]
        public void UserPostDTO_CanGet_Email()
        {
            // Arrange
            var userPostDTO = new UserPostDTO { Email = "test@example.com" };

            // Act & Assert
            Assert.Equal("test@example.com", userPostDTO.Email);
        }

        [Fact]
        public void UserPostDTO_CanSet_HotelList()
        {
            // Arrange
            var userPostDTO = new UserPostDTO();
            var hotelList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            userPostDTO.HotelList = hotelList;

            // Assert
            Assert.Equal(hotelList, userPostDTO.HotelList);
        }

        [Fact]
        public void UserPostDTO_HotelList_DefaultIsNull()
        {
            // Arrange & Act
            var userPostDTO = new UserPostDTO();

            // Assert
            Assert.Null(userPostDTO.HotelList);
        }

        [Fact]
        public void UserPostDTO_HotelList_CanInitialize()
        {
            // Arrange
            var userPostDTO = new UserPostDTO { HotelList = new List<Guid>() };

            // Act
            userPostDTO.HotelList.Add(Guid.NewGuid());

            // Assert
            Assert.NotEmpty(userPostDTO.HotelList);
        }
    }
}
