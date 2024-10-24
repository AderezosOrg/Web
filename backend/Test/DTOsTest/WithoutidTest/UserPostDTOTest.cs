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
    }
}
