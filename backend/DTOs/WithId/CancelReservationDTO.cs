namespace backend.DTOs.WithId;

public class CancelReservationDTO
{
    public Guid ContactID { get; set; }
    public Guid RoomID { get; set; }
}