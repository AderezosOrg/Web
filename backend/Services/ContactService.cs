using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

public class ContactService :
    IGetAllElementsService<ContactDTO>,
    IGetElementById<ContactPostDTO>,
    ICreateSingleElement<ContactPostDTO, ContactPostDTO>,
    IUpdateElementByID<ContactPostDTO, ContactPostDTO>
{
    private SingletonBD _singletonBd;
    
    private ContactPostConverter _contactPostConverter = new ContactPostConverter();
    private ContactConverter _contactConverter = new ContactConverter();

    public ContactService()
    {
        _singletonBd = SingletonBD.Instance;
    }
    public async Task<ContactPostDTO> GetElementById(Guid contactID)
    {
        await Task.Delay(10);
        var contact = _singletonBd.GetContactById(contactID);
        if (contact == null)
            throw new Exception("Contact not found");
        var reservation = _singletonBd.GetReservationByContactId(contactID);
        return _contactPostConverter.Convert(contact, reservation);
    }

    public async Task<List<ContactDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<ContactDTO> result = _singletonBd.GetAllContacts().Select(x =>
        {
            var reservation = _singletonBd.GetReservationByContactId(x.ContactID);
            return _contactConverter.Convert(x, reservation);
        }).ToList();
        
        return result;
    }

    public async Task<ContactPostDTO> CreateSingleElement(ContactPostDTO contactPostDtoDto)
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
             
            return contactPostDtoDto;
        }
        throw new Exception("Contact not data found");
    }

    public async Task<ContactPostDTO> UpdateElementById(Guid contactID, ContactPostDTO contactPostDtoDto)
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
