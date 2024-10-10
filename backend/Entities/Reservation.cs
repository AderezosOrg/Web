namespace DefaultNamespace;

public class Reservation
{
    public Guid UserID { get; set; }
    public Guid RoomID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime UseDate { get; set; }
    public bool Cancelled { get; set; }
}
