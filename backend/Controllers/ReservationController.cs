using backend.DTOs.WithId;
using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly ISessionService _sessionService;

    public ReservationController(IReservationService reservationService, ISessionService sessionService)
    {
        _reservationService = reservationService;
        _sessionService = sessionService;
    }
    
    [HttpGet("contact/{contactId}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservationByContactId(Guid contactId)
    {
        List<ReservationDTO> reservations = await _reservationService.GetReservationsByContactId(contactId);
        return Ok(reservations);
    }
    
    [HttpGet("room/{roomId}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservationByRoomId(Guid roomId)
    {
        List<ReservationDTO> reservations = await _reservationService.GetReservationsByRoomId(roomId);
        return Ok(reservations);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservations()
    {
        List<ReservationDTO> reservations = await _reservationService.GetAllElements();
        return Ok(reservations);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<ReservationDTO>>> CreateReservation( params ReservationPostDTO[] reservationDtos)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        List<ReservationDTO> reservations = await _reservationService.CreateReservation(reservationDtos);
        return Ok(reservations);    
    }
    
    [HttpPatch("cancel/{contactId}")]
    public async Task<ActionResult<ReservationDTO>> CancelReservation(CancelReservationDTO cancelReservationDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        ReservationDTO reservation = await _reservationService.CancelReservation(cancelReservationDto);
        return Ok(reservation);
    }
}
