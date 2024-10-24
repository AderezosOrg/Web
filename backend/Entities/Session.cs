namespace Entities;

public class Session
{
    public Guid SessionID { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }

    // Foreign keys
    public Guid ContactID { get; set; }
}
