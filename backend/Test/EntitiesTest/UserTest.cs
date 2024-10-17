using Xunit;
using Entities;
using System;

namespace backend.Test.EntitiesTest
{
    public class UserTest
    {
        [Fact]
        public void User_CanSet_UserID()
        {
            // Arrange
            var user = new User();
            var userId = Guid.NewGuid();

            // Act
            user.UserID = userId;

            // Assert
            Assert.Equal(userId, user.UserID);
        }

        [Fact]
        public void User_CanGet_UserID()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { UserID = userId };

            // Act & Assert
            Assert.Equal(userId, user.UserID);
        }

        [Fact]
        public void User_CanSet_Name()
        {
            // Arrange
            var user = new User();
            var userName = "John Doe";

            // Act
            user.Name = userName;

            // Assert
            Assert.Equal(userName, user.Name);
        }

        [Fact]
        public void User_CanGet_Name()
        {
            // Arrange
            var userName = "John Doe";
            var user = new User { Name = userName };

            // Act & Assert
            Assert.Equal(userName, user.Name);
        }

        [Fact]
        public void User_CanSet_CINumber()
        {
            // Arrange
            var user = new User();
            var ciNumber = "1234567890";

            // Act
            user.CINumber = ciNumber;

            // Assert
            Assert.Equal(ciNumber, user.CINumber);
        }

        [Fact]
        public void User_CanGet_CINumber()
        {
            // Arrange
            var ciNumber = "1234567890";
            var user = new User { CINumber = ciNumber };

            // Act & Assert
            Assert.Equal(ciNumber, user.CINumber);
        }

        [Fact]
        public void User_CanSet_ContactID()
        {
            // Arrange
            var user = new User();
            var contactId = Guid.NewGuid();

            // Act
            user.ContactID = contactId;

            // Assert
            Assert.Equal(contactId, user.ContactID);
        }

        [Fact]
        public void User_CanGet_ContactID()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var user = new User { ContactID = contactId };

            // Act & Assert
            Assert.Equal(contactId, user.ContactID);
        }

        [Fact]
        public void User_DefaultValues_AreDefault()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.Equal(Guid.Empty, user.UserID);
            Assert.Null(user.Name);
            Assert.Null(user.CINumber);
            Assert.Equal(Guid.Empty, user.ContactID);
        }
    }
}
