using Xunit;
using DTOs.WithId;
using Entities;
using Converters.ToDTO;

namespace backend.Test.ConvertersTest.ToDTOTest
{
    public class HotelConverterTests
    {
        private readonly HotelConverter _converter;

        public HotelConverterTests()
        {
            _converter = new HotelConverter();
        }

        [Fact]
        public void Convert_HotelUserContactBathroom_ReturnsHotelDTO()
        {
            // Arrange
            var hotel = new Hotel
            {
                HotelID = Guid.NewGuid(),
                Stars = 5,
                Name = "Luxury Hotel",
                AllowsPets = true,
                Address = "123 Luxury St."
            };

            var user = new User
            {
                UserID = Guid.NewGuid(),
                Name = "John Doe",
                CINumber = "C12345678"
            };

            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "987-654-3210",
                Email = "contact@luxuryhotel.com"
            };

            var bathroom = new Bathroom
            {
                BathRoomID = Guid.NewGuid(),
                Shower = true,
                Toilet = true,
                DressingTable = true
            };

            // Act
            var result = _converter.Convert(hotel, user, contact, bathroom);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotel.HotelID, result.HotelID);
            Assert.Equal(hotel.Stars, result.Stars);
            Assert.Equal(hotel.Name, result.Name);
            Assert.Equal(hotel.AllowsPets, result.AllowsPets);
            Assert.Equal(hotel.Address, result.Address);
            Assert.Equal(user.Name, result.UserName);
            Assert.Equal(user.CINumber, result.UserCINumber);
            Assert.Equal(contact.PhoneNumber, result.HotelPhoneNumber);
            Assert.Equal(contact.Email, result.HotelEmail);
            Assert.Equal(bathroom.Shower, result.Shower);
            Assert.Equal(bathroom.Toilet, result.Toilet);
            Assert.Equal(bathroom.DressingTable, result.DressingTable);
        }
    }
}
