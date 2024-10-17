using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class HotelPostDTOTest
    {
        [Fact]
        public void HotelPostDTO_CanSet_Stars()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            int stars = 5;

            // Act
            hotelPostDTO.Stars = stars;

            // Assert
            Assert.Equal(stars, hotelPostDTO.Stars);
        }

        [Fact]
        public void HotelPostDTO_CanGet_Stars()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { Stars = 3 };

            // Act & Assert
            Assert.Equal(3, hotelPostDTO.Stars);
        }

        [Fact]
        public void HotelPostDTO_CanSet_Name()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string name = "Test Hotel";

            // Act
            hotelPostDTO.Name = name;

            // Assert
            Assert.Equal(name, hotelPostDTO.Name);
        }

        [Fact]
        public void HotelPostDTO_CanGet_Name()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { Name = "Sample Hotel" };

            // Act & Assert
            Assert.Equal("Sample Hotel", hotelPostDTO.Name);
        }

        [Fact]
        public void HotelPostDTO_CanSet_AllowsPets()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            bool allowsPets = true;

            // Act
            hotelPostDTO.AllowsPets = allowsPets;

            // Assert
            Assert.Equal(allowsPets, hotelPostDTO.AllowsPets);
        }

        [Fact]
        public void HotelPostDTO_CanGet_AllowsPets()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { AllowsPets = false };

            // Act & Assert
            Assert.False(hotelPostDTO.AllowsPets);
        }

        [Fact]
        public void HotelPostDTO_CanSet_Address()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string address = "123 Main St";

            // Act
            hotelPostDTO.Address = address;

            // Assert
            Assert.Equal(address, hotelPostDTO.Address);
        }

        [Fact]
        public void HotelPostDTO_CanGet_Address()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { Address = "456 Elm St" };

            // Act & Assert
            Assert.Equal("456 Elm St", hotelPostDTO.Address);
        }

        [Fact]
        public void HotelPostDTO_CanSet_UserName()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string userName = "John Doe";

            // Act
            hotelPostDTO.UserName = userName;

            // Assert
            Assert.Equal(userName, hotelPostDTO.UserName);
        }

        [Fact]
        public void HotelPostDTO_CanGet_UserName()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { UserName = "Jane Doe" };

            // Act & Assert
            Assert.Equal("Jane Doe", hotelPostDTO.UserName);
        }

        [Fact]
        public void HotelPostDTO_CanSet_UserCINumber()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string userCINumber = "CIN12345";

            // Act
            hotelPostDTO.UserCINumber = userCINumber;

            // Assert
            Assert.Equal(userCINumber, hotelPostDTO.UserCINumber);
        }

        [Fact]
        public void HotelPostDTO_CanGet_UserCINumber()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { UserCINumber = "CIN54321" };

            // Act & Assert
            Assert.Equal("CIN54321", hotelPostDTO.UserCINumber);
        }

        [Fact]
        public void HotelPostDTO_CanSet_UserPhoneNumber()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string userPhoneNumber = "321-654-0987";

            // Act
            hotelPostDTO.UserPhoneNumber = userPhoneNumber;

            // Assert
            Assert.Equal(userPhoneNumber, hotelPostDTO.UserPhoneNumber);
        }

        [Fact]
        public void HotelPostDTO_CanGet_UserPhoneNumber()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { UserPhoneNumber = "765-432-1098" };

            // Act & Assert
            Assert.Equal("765-432-1098", hotelPostDTO.UserPhoneNumber);
        }

        [Fact]
        public void HotelPostDTO_CanSet_UserEmail()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string userEmail = "user@example.com";

            // Act
            hotelPostDTO.UserEmail = userEmail;

            // Assert
            Assert.Equal(userEmail, hotelPostDTO.UserEmail);
        }

        [Fact]
        public void HotelPostDTO_CanGet_UserEmail()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { UserEmail = "test@example.com" };

            // Act & Assert
            Assert.Equal("test@example.com", hotelPostDTO.UserEmail);
        }

        [Fact]
        public void HotelPostDTO_CanSet_HotelPhoneNumber()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string hotelPhoneNumber = "555-123-4567";

            // Act
            hotelPostDTO.HotelPhoneNumber = hotelPhoneNumber;

            // Assert
            Assert.Equal(hotelPhoneNumber, hotelPostDTO.HotelPhoneNumber);
        }

        [Fact]
        public void HotelPostDTO_CanGet_HotelPhoneNumber()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { HotelPhoneNumber = "555-765-4321" };

            // Act & Assert
            Assert.Equal("555-765-4321", hotelPostDTO.HotelPhoneNumber);
        }

        [Fact]
        public void HotelPostDTO_CanSet_HotelEmail()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            string hotelEmail = "hotel@example.com";

            // Act
            hotelPostDTO.HotelEmail = hotelEmail;

            // Assert
            Assert.Equal(hotelEmail, hotelPostDTO.HotelEmail);
        }

        [Fact]
        public void HotelPostDTO_CanGet_HotelEmail()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { HotelEmail = "contact@hotel.com" };

            // Act & Assert
            Assert.Equal("contact@hotel.com", hotelPostDTO.HotelEmail);
        }

        [Fact]
        public void HotelPostDTO_CanSet_Shower()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            bool shower = true;

            // Act
            hotelPostDTO.Shower = shower;

            // Assert
            Assert.Equal(shower, hotelPostDTO.Shower);
        }

        [Fact]
        public void HotelPostDTO_CanGet_Shower()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { Shower = false };

            // Act & Assert
            Assert.False(hotelPostDTO.Shower);
        }

        [Fact]
        public void HotelPostDTO_CanSet_Toilet()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            bool toilet = true;

            // Act
            hotelPostDTO.Toilet = toilet;

            // Assert
            Assert.Equal(toilet, hotelPostDTO.Toilet);
        }

        [Fact]
        public void HotelPostDTO_CanGet_Toilet()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { Toilet = false };

            // Act & Assert
            Assert.False(hotelPostDTO.Toilet);
        }

        [Fact]
        public void HotelPostDTO_CanSet_DressingTable()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO();
            bool dressingTable = true;

            // Act
            hotelPostDTO.DressingTable = dressingTable;

            // Assert
            Assert.Equal(dressingTable, hotelPostDTO.DressingTable);
        }

        [Fact]
        public void HotelPostDTO_CanGet_DressingTable()
        {
            // Arrange
            var hotelPostDTO = new HotelPostDTO { DressingTable = false };

            // Act & Assert
            Assert.False(hotelPostDTO.DressingTable);
        }

        [Fact]
        public void HotelPostDTO_DefaultValues_AreNullOrFalse()
        {
            // Arrange & Act
            var hotelPostDTO = new HotelPostDTO();

            // Assert
            Assert.Equal(0, hotelPostDTO.Stars);
            Assert.Null(hotelPostDTO.Name);
            Assert.False(hotelPostDTO.AllowsPets);
            Assert.Null(hotelPostDTO.Address);
            Assert.Null(hotelPostDTO.UserName);
            Assert.Null(hotelPostDTO.UserCINumber);
            Assert.Null(hotelPostDTO.UserPhoneNumber);
            Assert.Null(hotelPostDTO.UserEmail);
            Assert.Null(hotelPostDTO.HotelPhoneNumber);
            Assert.Null(hotelPostDTO.HotelEmail);
            Assert.False(hotelPostDTO.Shower);
            Assert.False(hotelPostDTO.Toilet);
            Assert.False(hotelPostDTO.DressingTable);
        }
    }
}
