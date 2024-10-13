using DTOs.WithId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;
[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    [HttpGet("{contactId}")]
    public ActionResult<ContactDTO> GetContactById(Guid contactId)
    {
        return Ok(new ContactDTO
        {
            ContactID = Guid.NewGuid(),
            Email = "email@email.com",
            PhoneNumber = "+35912345678",
            ReservationList = new List<Guid>
            {
                Guid.NewGuid()
            }
        });
    }
    
    [HttpGet]
    public ActionResult<List<ContactDTO>> GetContacts()
    {
        return Ok(new List<ContactDTO>
        {
            new ContactDTO
            {
                ContactID = Guid.NewGuid(),
                Email = "email@email.com",
                PhoneNumber = "+35912345678",
                ReservationList = new List<Guid>
                {
                    Guid.NewGuid()
                }
            }
        });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateContact([FromBody] ContactDTO contactDto)
    {
        return Ok(true);
    }
    
    [HttpPut("{contactId}")]
    public ActionResult<bool> ChangeContact(Guid contactId, ContactDTO contactDto)
    {
        return Ok(true);
    }
}
