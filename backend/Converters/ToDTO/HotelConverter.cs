using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class HotelConverter : IConverter1To4<Hotel, User, Contact, Bathroom, HotelDTO>
    {
        public HotelDTO Convert(Hotel hotel, User user, Contact contact, Bathroom bathroom)
        {
            return new HotelDTO
            {
                HotelID = hotel.HotelID,
                Stars = hotel.Stars,
                Name = hotel.Name,
                AllowsPets = hotel.AllowsPets,
                Address = hotel.Address,
                UserName = user.Name,
                UserCINumber = user.CINumber,
                HotelPhoneNumber = contact.PhoneNumber,
                HotelEmail = contact.Email,
                Shower = bathroom.Shower,
                Toilet = bathroom.Toilet,
                DressingTable = bathroom.DressingTable
            };
        }
    }
}
