namespace DTOs.WithoutId;

public class ContactPostDTO
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Guid> ReservationList { get; set; }
}
