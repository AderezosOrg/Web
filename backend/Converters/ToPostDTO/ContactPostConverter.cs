using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class ContactPostConverter : IConverter1To1<Contact, ContactPostDTO>
{
    public ContactPostDTO Convert(Contact contact)
    {

        return new ContactPostDTO
        {
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email
        };
    }
}
