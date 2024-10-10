namespace DefaultNamespace;

public class User
{
    public Guid UserID { get; set; }
    public string Name { get; set; }
    public string CINumber { get; set; }

    // Foreign key
    public Guid ContactID { get; set; }
}
