using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services.AbstractClass;

public abstract class AbstractReservationService
{
    public abstract Task<List<ReservationDTO>> GetReservationsByContactId(Guid contactId);
    public abstract Task<List<ReservationDTO>> GetReservationsByRoomId(Guid roomId);
    public abstract Task<List<ReservationDTO>> GetReservations();
    public abstract Task<ReservationDTO> CreateReservation(ReservationDTO reservationDto);
    public abstract Task<ReservationDTO> CancelReservation(Guid reservationId);
}
