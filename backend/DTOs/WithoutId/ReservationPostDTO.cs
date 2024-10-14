namespace DTOs.WithoutId;

public class ReservationPostDTO
{
    // Reservation properties
    public DateTime ReservationDate { get; set; }
    public DateTime UseDate { get; set; }
    public bool Cancelled { get; set; }

    public Guid RoomId { get; set; }
    public Guid ContactId { get; set; }
}