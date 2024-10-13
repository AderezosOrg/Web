using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO;
public class UserConverter : IConverter1To3<User, Contact, List<Hotel>, UserDTO>
{
    public UserDTO Convert(User user, Contact contact, List<Hotel> hotels)
    {
        var hotelList = hotels
            .Where(h => h.ContactID == contact.ContactID) 
            .Select(h => h.HotelID) 
            .ToList();
        
        return new UserDTO
        {
            UserID = user.UserID,
            Name = user.Name,
            CINumber = user.CINumber,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
            HotelList = hotelList
        };
    }
}
