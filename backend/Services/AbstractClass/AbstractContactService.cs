using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractContactService
{
    public abstract Task<ContactPostDTO> GetContactById(Guid contactID);
    public abstract Task<List<ContactPostDTO>> GetContacts();
    public abstract Task<ContactPostDTO> CreateContact(ContactPostDTO contactDto);
    public abstract Task<ContactPostDTO> ChangeContact(Guid contactID, ContactPostDTO contactDto);
}