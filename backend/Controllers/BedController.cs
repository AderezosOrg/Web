using DTOs.WithId;
using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BedController : ControllerBase
{
    private readonly IBedService _bedService;
    private readonly ISessionService _sessionService;
    public BedController(IBedService bedService, ISessionService sessionService)
    {
        _bedService = bedService;
        _sessionService = sessionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BedInfoDTO>>> GetBeds()
    {
        
        var beds = await _bedService.GetAllElements();
        return Ok(beds);
        
    }
    
    [HttpGet("{bedId}")]
    public  async Task<ActionResult<BedPostDTO>> GetBedById(Guid bedId)
    {
        var bed = await _bedService.GetElementById(bedId);
        return Ok(bed);
    }
    
    [HttpPost]
    public async Task<ActionResult<BedPostDTO>> CreateBed(BedPostDTO bedDTO)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var bed = await _bedService.CreateSingleElement(bedDTO);
        return Ok(bed);
    }
    
    [HttpPut("{BedID}")]
    public async Task<ActionResult<BedPostDTO>> EditBed(Guid BedID, BedPostDTO bedDTO)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var bed = await _bedService.UpdateElementById(BedID, bedDTO);
        return Ok(bed);
    }
    
    [HttpDelete("{BedID}")]
    public async Task<ActionResult<bool>> DeleteBed(Guid BedID)
    {
        var confirm = await _bedService.DeleteElementById(BedID);
        return Ok(confirm);
    }
}
