namespace Entities;

public class Reservation
{
    public Guid ContactID { get; set; }
    public Guid RoomID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime UseDate { get; set; }
    public bool Cancelled { get; set; }
}
