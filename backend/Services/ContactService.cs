using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using backend.Services.AbstractClass;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

public class ContactService : AbstractContactService
{
    private SingletonBD _singletonBd;
    
    private ContactPostConverter _contactPostConverter = new ContactPostConverter();
    private ContactConverter _contactConverter = new ContactConverter();

    public ContactService()
    {
        _singletonBd = SingletonBD.Instance;
    }
    public override async Task<ContactPostDTO> GetContactById(Guid contactID)
    {
        await Task.Delay(10);
        var contact = _singletonBd.GetContactById(contactID);
        if (contact == null)
            throw new Exception("Contact not found");
        var reservation = _singletonBd.GetReservationByContactId(contactID);
        return _contactPostConverter.Convert(contact, reservation);
    }

    public override async Task<List<ContactDTO>> GetContacts()
    {
        await Task.Delay(10);
        List<ContactDTO> result = _singletonBd.GetAllContacts().Select(x =>
        {
            var reservation = _singletonBd.GetReservationByContactId(x.ContactID);
            return _contactConverter.Convert(x, reservation);
        }).ToList();
        
        return result;
    }

    public override async Task<ContactPostDTO> CreateContact(ContactPostDTO contactPostDtoDto)
    {
        await Task.Delay(100);
        if (contactPostDtoDto != null)
        {
            var newContact = new Contact
            {
                ContactID = Guid.NewGuid(),
                Email = contactPostDtoDto.Email,
                PhoneNumber = contactPostDtoDto.PhoneNumber
            };
            _singletonBd.AddContact(newContact);
            if(_singletonBd.GetAllContacts().Contains(newContact))
                return contactPostDtoDto;
            else
                throw new Exception("Contact not created");
        }
        throw new Exception("Contact not data found");
    }

    public override async Task<ContactPostDTO> ChangeContact(Guid contactID, ContactPostDTO contactPostDtoDto)
    {
        await Task.Delay(100);
        var reservation = _singletonBd.GetAllReservations();
        return _contactPostConverter.Convert(_singletonBd.UpdateContact(new Contact()
        {
            ContactID = contactID,
            Email = contactPostDtoDto.Email,
            PhoneNumber = contactPostDtoDto.PhoneNumber
        }), reservation);
    }
}
