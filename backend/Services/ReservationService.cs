using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.Interfaces;

namespace backend.Services;

public class ReservationService : IReservationService
{
    private List<ReservationDTO> _reservations = new List<ReservationDTO>
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
    };

    public ReservationDTO GetReservationById(Guid reservationId)
    {
        return _reservations.FirstOrDefault(r => r.RoomID == reservationId);
    }

    public List<ReservationDTO> GetReservations()
    {
        return _reservations;
    }

    public bool CreateReservation(ReservationDTO reservationDto)
    {
        _reservations.Add(reservationDto);
        return true;
    }

    public bool CancelReservation(Guid reservationId, bool available)
    {
        var reservation = _reservations.FirstOrDefault(r => r.RoomID == reservationId);
        if (reservation != null)
        {
            reservation.Cancelled = true;
            reservation.RoomAvailable = available;
            return true;
        }
        return false;
    }
}
