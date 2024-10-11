using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    [HttpGet("{reservationId}")]
    public ActionResult<ReservationDTO> GetReservationById(Guid reservationId)
    {
        return Ok(new ReservationDTO
        {
            Cancelled = true,
            ContactID = Guid.NewGuid(),
            ReservationDate = DateTime.Today,
            RoomAvailable = false,
            RoomCode = "wqe",
            RoomFloorNumber = 2,
            RoomID = Guid.NewGuid(),
            UseDate = DateTime.Now,
            UserEmail = "test@test2.com",
            UserPhoneNumber = "12345687"
        });
    }
    
    [HttpGet]
    public ActionResult<List<ReservationDTO>> GetReservations()
    {
        return Ok(new List<ReservationDTO>
        {
            new ReservationDTO
            {
                Cancelled = false,
                ContactID = Guid.NewGuid(),
                ReservationDate = DateTime.Now,
                RoomID = Guid.NewGuid(),
                RoomAvailable = true,
                RoomCode = "10A",
                RoomFloorNumber = 1,
                UseDate = DateTime.Today,
                UserEmail = "test@test.com",
                UserPhoneNumber = "12345678"
            }
        });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateReservation(ReservationDTO reservationDto)
    {
        return true;
    }
    
    [HttpPatch("{reservationId}")]
    public ActionResult<bool> CancelReservation(Guid reservationId, bool Available)
    {
        return true;
    }
}
