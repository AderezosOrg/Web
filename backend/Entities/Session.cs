namespace Entities;

public class Session
{
    public Guid SessionId { get; set; }
    public string Token { get; set; }

    // Foreign keys
    public Guid RoomID { get; set; }
}