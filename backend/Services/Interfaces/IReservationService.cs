namespace backend.Services.Interfaces;
using DTOs;

public interface IReservationService
{
    ReservationDTO GetReservationById(Guid reservationId);
    List<ReservationDTO> GetReservations();
    bool CreateReservation(ReservationDTO reservationDto);
    bool CancelReservation(Guid reservationId, bool available);
}
