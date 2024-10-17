using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services.ServicesInterfaces;

public interface IReservationService
{
    public Task<List<ReservationDTO>> GetReservationsByContactId(Guid contactId);
    public Task<List<ReservationDTO>> GetReservationsByRoomId(Guid roomId);
    public Task<List<ReservationDTO>> CreateReservation(params ReservationPostDTO[] reservationDto);
    public Task<ReservationDTO> CancelReservation(Guid reservationId);
}
