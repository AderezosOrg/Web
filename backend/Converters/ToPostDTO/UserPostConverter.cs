using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class UserPostConverter : IConverter1To3<User, Contact, List<Hotel>, UserPostDTO>
{
    public UserPostDTO Convert(User user, Contact contact, List<Hotel> hotels)
    {
        var hotelList = hotels
            .Where(h => h.ContactID == contact.ContactID) 
            .Select(h => h.HotelID) 
            .ToList();
        
        return new UserPostDTO()
        {
            Name = user.Name,
            CINumber = user.CINumber,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
            HotelList = hotelList
        };
    }
    
}