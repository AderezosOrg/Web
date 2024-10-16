using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
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
        List<ReservationDTO> reservations = await _reservationService.GetReservations();
        return Ok(reservations);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<ReservationDTO>>> CreateReservation( params ReservationPostDTO[] reservationDtos)
    {
        List<ReservationDTO> reservations = await _reservationService.CreateReservation(reservationDtos);
        return Ok(reservations);
        
    }
    
    [HttpPatch("cancel/{contactId}")]
    public async Task<ActionResult<ReservationDTO>> CancelReservation(Guid contactId)
    {
        ReservationDTO reservation = await _reservationService.CancelReservation(contactId);
        return Ok(reservation);
    }
}
