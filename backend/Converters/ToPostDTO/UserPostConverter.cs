using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class UserPostConverter : IConverter1To2<User, Contact, UserPostDTO>
{
    public UserPostDTO Convert(User user, Contact contact)
    {
        return new UserPostDTO()
        {
            Name = user.Name,
            CINumber = user.CINumber,
            ContactId = contact.ContactID,
        };
    }
    
}