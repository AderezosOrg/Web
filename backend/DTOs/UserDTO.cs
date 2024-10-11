namespace DTOs;

public class UserDTO
{
    public Guid UserID { get; set; }
    public string Name { get; set; }
    public string CINumber { get; set; }
    
    // Contact properties (from FK) 
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
