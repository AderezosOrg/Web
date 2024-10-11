namespace DTOs;

public class ContactDTO
{
    public Guid ContactID { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Guid> ReservationList { get; set; }
}
