using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactService _contactService;
    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }
    [HttpGet("{contactId}")]
    public async Task<ActionResult<ContactPostDTO>> GetContactById(Guid contactId)
    {
        ContactPostDTO contact = await _contactService.GetElementById(contactId);
        return Ok(contact);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ContactDTO>>> GetContacts()
    {
        List<ContactDTO> contacts = await _contactService.GetAllElements();
        return Ok(contacts);
    }
    
    [HttpPost]
    public async Task<ActionResult<ContactDTO>> CreateContact([FromBody] ContactPostDTO contactDto)
    {
        ContactDTO contact = await _contactService.CreateSingleElement(contactDto);
        return Ok(contact);
    }
    
    [HttpPut("{contactId}")]
    public async Task<ActionResult<ContactDTO>> ChangeContact(Guid contactId, ContactPostDTO contactDto)
    {
        ContactDTO contact = await _contactService.UpdateElementById(contactId, contactDto);
        return Ok(contact);
    }
}
