using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using Db;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

public class ContactService : IContactService
{
    private IDAO<Contact> _contactDao;
    private IReservationDAO _reservationDao;
    
    private ContactPostConverter _contactPostConverter = new ContactPostConverter();
    private ContactConverter _contactConverter = new ContactConverter();

    public ContactService(IDAO<Contact> contactDao, IReservationDAO reservationDao)
    {
        _reservationDao = reservationDao;
        _contactDao = contactDao;
    }
    public async Task<ContactPostDTO> GetElementById(Guid contactID)
    {
        await Task.Delay(10);
        var contact = _contactDao.Read(contactID);
        if (contact == null)
            throw new Exception("Contact not found");
        var reservation = _reservationDao.GetReservationsByContactId(contactID);
        return _contactPostConverter.Convert(contact);
    }

    public async Task<List<ContactDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<ContactDTO> result = _contactDao.ReadAll().Select(x =>
        {
            var contactID = x.ContactID;
            var reservation = _reservationDao.GetReservationsByContactId(contactID);
            return _contactConverter.Convert(x);
        }).ToList();
        
        return result;
    }

    public async Task<ContactDTO> CreateSingleElement(ContactPostDTO contactPostDtoDto)
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
            _contactDao.Create(newContact);
            return _contactConverter.Convert(newContact);
        }
        throw new Exception("Contact not data found");
    }

    public async Task<ContactDTO> UpdateElementById(Guid contactID, ContactPostDTO contactPostDtoDto)
    {
        await Task.Delay(100);
        var contact = new Contact()
        {
            ContactID = contactID,
            Email = contactPostDtoDto.Email,
            PhoneNumber = contactPostDtoDto.PhoneNumber
        };
        _contactDao.Update(contact);
        return _contactConverter.Convert(contact);
    }
}
