namespace DTOs.WithId;

public class ReservationDTO
{
    // Reservation properties
    public Guid ContactID { get; set; }
    public Guid RoomID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime UseDate { get; set; }
    public bool Cancelled { get; set; }

    // Contact properties (from FK) 
    public string UserPhoneNumber { get; set; }
    public string UserEmail { get; set; }

    // Room properties (from FK)
    public string RoomCode { get; set; }
    public int RoomFloorNumber { get; set; }
    public decimal PricePerNight { get; set; }
}
