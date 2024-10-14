using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class HotelPostConverter : IConverter1To4<Hotel, User, Contact, Bathroom, HotelPostDTO>
{
    public HotelPostDTO Convert(Hotel hotel, User user, Contact contact, Bathroom bathroom)
    {
        return new HotelPostDTO
        {
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
            DressingTable = bathroom.DressingTable,
            Tax = hotel.Tax
        };
    }
}
