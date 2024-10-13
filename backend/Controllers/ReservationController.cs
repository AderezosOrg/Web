using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
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
    [HttpGet("{reservationId}")]
    public async Task<ActionResult<ReservationDTO>> GetReservationById(Guid reservationId)
    {
        ReservationDTO reservation = await _reservationService.GetReservationById(reservationId);
        return Ok(reservation);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservations()
    {
        List<ReservationDTO> reservations = await _reservationService.GetReservations();
        return Ok(reservations);
    }
    
    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> CreateReservation(ReservationDTO reservationDto)
    {
        ReservationDTO reservation = await _reservationService.CreateReservation(reservationDto);
        return Ok(reservation);
        
    }
    
    [HttpPatch("{reservationId}")]
    public async Task<ActionResult<ReservationDTO>> CancelReservation(Guid reservationId)
    {
        ReservationDTO reservation = await _reservationService.CancelReservation(reservationId);
        return Ok(reservation);
    }
}
