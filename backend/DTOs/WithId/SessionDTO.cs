namespace DTOs.WithoutId;

public class SessionDTO
{
    public Guid SessionId { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }
}