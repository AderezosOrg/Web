using Xunit;
using DTOs.WithoutId;
using Entities;
using backend.Converters.ToPostDTO;

namespace backend.Test.ConvertersTest.ToPostDTOTest
{
    public class HotelPostConverterTests
    {
        private readonly HotelPostConverter _converter;

        public HotelPostConverterTests()
        {
            _converter = new HotelPostConverter();
        }

        [Fact]
        public void Convert_ValidHotelAndRelatedEntities_ReturnsHotelPostDTO()
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
                CINumber = "C123456789"
            };

            var contact = new Contact
            {
                ContactID = Guid.NewGuid(),
                PhoneNumber = "555-1234",
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
