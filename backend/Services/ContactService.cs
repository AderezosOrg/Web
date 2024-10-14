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
    private List<Contact> _contact = new List<Contact>();
    
    private List<Reservation> _reservations = new List<Reservation>();
    
    private ContactPostConverter _contactPostConverter = new ContactPostConverter();
    private ContactConverter _contactConverter = new ContactConverter();

    public ContactService()
    {
        _singletonBd = SingletonBD.Instance;
        _contact = _singletonBd.GetAllContacts();
        _reservations = _singletonBd.GetAllReservations();
    }
    public override async Task<ContactPostDTO> GetContactById(Guid contactID)
    {
        await Task.Delay(10);
        var contact = _singletonBd.GetContactById(contactID);
        if (contact == null)
            throw new Exception("Contact not found");
        var reservation = _reservations.Where(x => x.ContactID == contact.ContactID).ToList();
        return _contactPostConverter.Convert(contact, reservation);
    }

    public override async Task<List<ContactDTO>> GetContacts()
    {
        await Task.Delay(10);
        _contact = _singletonBd.GetAllContacts();
        List<ContactDTO> result = _contact.Select(x =>
        {
            var reservation = _reservations.Where(x => x.ContactID == x.ContactID).ToList();
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
