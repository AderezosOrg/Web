using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.Interfaces;

namespace backend.Services;

public class ContactService : IContactService
{
    private List<ContactDTO> _contacts = new List<ContactDTO>
    {
        new ContactDTO
        {
            ContactID = Guid.NewGuid(),
            Email = "email@email.com",
            PhoneNumber = "+35912345678",
            ReservationList = new List<Guid> { Guid.NewGuid() }
        }
    };

    public ContactDTO GetContactById(Guid contactID)
    {
        return _contacts.FirstOrDefault(c => c.ContactID == contactID);
    }

    public List<ContactDTO> GetContacts()
    {
        return _contacts;
    }

    public bool CreateContact(ContactDTO contactDto)
    {
        _contacts.Add(contactDto);
        return true;
    }

    public bool ChangeContact(Guid contactID, ContactDTO contactDto)
    {
        var existingContact = _contacts.FirstOrDefault(c => c.ContactID == contactID);
        if (existingContact != null)
        {
            existingContact.Email = contactDto.Email;
            existingContact.PhoneNumber = contactDto.PhoneNumber;
            existingContact.ReservationList = contactDto.ReservationList;
            return true;
        }
        return false;
    }
}
