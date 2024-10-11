namespace backend.Services.Interfaces;
using DTOs;

public interface IContactService
{
    ContactDTO GetContactById(Guid contactID);
    List<ContactDTO> GetContacts();
    bool CreateContact(ContactDTO contactDto);
    bool ChangeContact(Guid contactID, ContactDTO contactDto);
}
