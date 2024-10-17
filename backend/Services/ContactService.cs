using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using Db;
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
    private ContactDAO _contactDao;
    private ReservationDAO _reservationDao;
    
    private ContactPostConverter _contactPostConverter = new ContactPostConverter();
    private ContactConverter _contactConverter = new ContactConverter();

    public ContactService(ContactDAO contactDao, ReservationDAO reservationDao)
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
        return _contactPostConverter.Convert(contact, reservation);
    }

    public async Task<List<ContactDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<ContactDTO> result = _contactDao.ReadAll().Select(x =>
        {
            var contactID = x.ContactID;
            var reservation = _reservationDao.GetReservationsByContactId(contactID);
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
            _contactDao.Create(newContact);
            return contactPostDtoDto;
        }
        throw new Exception("Contact not data found");
    }

    public async Task<ContactPostDTO> UpdateElementById(Guid contactID, ContactPostDTO contactPostDtoDto)
    {
        await Task.Delay(100);
        var reservation = _reservationDao.GetReservationsByContactId(contactID);
        var contact = new Contact()
        {
            ContactID = contactID,
            Email = contactPostDtoDto.Email,
            PhoneNumber = contactPostDtoDto.PhoneNumber
        };
        _contactDao.Update(contact);
        return _contactPostConverter.Convert(contact, reservation);
    }
}
