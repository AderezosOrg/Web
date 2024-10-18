using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO;
public class UserConverter : IConverter1To2<User, Contact, UserDTO>
{
    public UserDTO Convert(User user, Contact contact)
    {
        
        return new UserDTO
        {
            UserID = user.UserID,
            Name = user.Name,
            CINumber = user.CINumber,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
        };
    }
}
