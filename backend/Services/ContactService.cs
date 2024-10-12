using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Converters.ToPostDTO;
using backend.Services.AbstractClass;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

public class ContactService : AbstractContactService
{
    private static List<Contact> _contact = new List<Contact>()
    {
        new Contact()
        {
            ContactID = Guid.NewGuid(),
            Email = "email@email.com",
            PhoneNumber = "+35912345678",
        },
        new Contact()
        {
            ContactID = Guid.NewGuid(),
            Email = "LEONARDO@email.com",
            PhoneNumber = "+59174365698",
        }
    };

    private List<Reservation> _reservations = new List<Reservation>()
    {
        new Reservation()
        {
            Cancelled = false,
            ContactID = _contact[0].ContactID,
            ReservationDate = DateTime.Now,
            RoomID = Guid.NewGuid(),
            UseDate = DateTime.Today
        },
        new Reservation()
        {
            Cancelled = true,
            ContactID = _contact[1].ContactID,
            ReservationDate = DateTime.Now,
            RoomID = Guid.NewGuid(),
            UseDate = DateTime.Today
        }
    };
    
    private ContactPostConverter _contactPostConverter = new ContactPostConverter();
    private ContactConverter _contactConverter = new ContactConverter();
    public override async Task<ContactPostDTO> GetContactById(Guid contactID)
    {
        await Task.Delay(10);
        var contact = _contact.FirstOrDefault(x => x.ContactID == contactID);
        if (contact == null)
            throw new Exception("Contact not found");
        var reservation = _reservations.Where(x => x.ContactID == contact.ContactID).ToList();
        return _contactPostConverter.Convert(contact, reservation);
    }

    public override async Task<List<ContactDTO>> GetContacts()
    {
        await Task.Delay(10);
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
            _contact.Add(newContact);

            var reservations = new Reservation
            {
                Cancelled = false,
                ContactID = newContact.ContactID,
                ReservationDate = DateTime.Now,
                RoomID = Guid.NewGuid(),
                UseDate = DateTime.Today
            };
            _reservations.Add(reservations);
            if(_contact.Contains(newContact) && _reservations.Contains(reservations))
                return contactPostDtoDto;
            else
                throw new Exception("Contact not created");
        }
        throw new Exception("Contact not data found");
    }

    public override async Task<ContactPostDTO> ChangeContact(Guid contactID, ContactPostDTO contactPostDtoDto)
    {
        await Task.Delay(100);
        var existingContact = _contact.FirstOrDefault(x => x.ContactID == contactID);
        if (existingContact != null)
        {
            existingContact.Email = contactPostDtoDto.Email;
            existingContact.PhoneNumber = contactPostDtoDto.PhoneNumber;
            return contactPostDtoDto;
        }
        throw new Exception("Contact not found");
    }
}
