using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class ContactConverter : IConverter1To1<Contact, ContactDTO>
    {
        public ContactDTO Convert(Contact contact)
        {
            return new ContactDTO
            {
                ContactID = contact.ContactID,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email
            };
        }
    }
}
