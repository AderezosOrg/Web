namespace DTOs.WithId;

public class SessionFullInfoDTO
{
    public Guid SessionID { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid ContactID { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
