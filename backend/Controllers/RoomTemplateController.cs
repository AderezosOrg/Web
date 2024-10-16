using backend.MyHappyBD;
using backend.Services.AbstractClass;
using Converters.ToDTO;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomTemplateController : ControllerBase
{
    private RoomTemplateService _roomTemplateService;

    public RoomTemplateController(RoomTemplateService roomTemplateService)
    {
        _roomTemplateService = roomTemplateService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomTemplateDTO>>> GetAllRoomTemplates()
    {
        var roomTemplates = await _roomTemplateService.GetAllElements();
        return Ok(roomTemplates);
    }
    
    [HttpGet("{templateid}")]
    public async Task<ActionResult<RoomTemplateDTO>> GetRoomTemplateById(Guid templateid)
    {
        var roomTemplate = await _roomTemplateService.GetElementById(templateid);
        return Ok(roomTemplate);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<RoomTemplateDTO>>> PostRoomTemplate([FromBody] RoomTemplatePostDTO roomTemplatePostDto)
    {
        var roomTemplate = await _roomTemplateService.CreateSingleElement(roomTemplatePostDto);
        return Ok(roomTemplate);
    }
    
    [HttpPut("{roomTemplateid}")]
    public async Task<ActionResult<List<RoomTemplateDTO>>> EditRoomTemplate(Guid roomTemplateId, [FromBody] RoomTemplatePostDTO roomTemplatePostDto)
    {
        var roomTemplate = await _roomTemplateService.UpdateElementById(roomTemplateId, roomTemplatePostDto);
        return Ok(roomTemplate);
    }
}
