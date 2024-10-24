using backend.Services.ServicesInterfaces;
using Converters.ToDTO;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomTemplateController : ControllerBase
{
    private readonly IRoomTemplateService _roomTemplateService;
    private readonly ISessionService _sessionService;

    public RoomTemplateController(IRoomTemplateService roomTemplateService, ISessionService sessionService)
    {
        _roomTemplateService = roomTemplateService;
        _sessionService = sessionService;
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
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var roomTemplate = await _roomTemplateService.CreateSingleElement(roomTemplatePostDto);
        return Ok(roomTemplate);
    }
    
    [HttpPut("{roomTemplateid}")]
    public async Task<ActionResult<List<RoomTemplateDTO>>> EditRoomTemplate(Guid roomTemplateId, [FromBody] RoomTemplatePostDTO roomTemplatePostDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var roomTemplate = await _roomTemplateService.UpdateElementById(roomTemplateId, roomTemplatePostDto);
        return Ok(roomTemplate);
    }
}
