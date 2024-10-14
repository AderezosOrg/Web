using Xunit;
using DTOs.WithId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class UserPostConverterTests
    {
        private readonly UserPostConverter _converter;

        public UserPostConverterTests()
        {
            _converter = new UserPostConverter();
        }

        [Fact]
        public void Convert_ValidUserAndContact_ReturnsUserPostDTO()
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
                Email = "john.doe@example.com"
            };

            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    HotelID = Guid.NewGuid(),
                    Name = "Hotel A",
                    ContactID = contact.ContactID
                },
                new Hotel
                {
                    HotelID = Guid.NewGuid(),
                    Name = "Hotel B",
                    ContactID = Guid.NewGuid() 
                }
            };

            // Act
            var result = _converter.Convert(user, contact, hotels);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.CINumber, result.CINumber);
            Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
            Assert.Equal(contact.Email, result.Email);
            Assert.Single(result.HotelList); 
            Assert.Contains(hotels[0].HotelID, result.HotelList);
        }
    }
}