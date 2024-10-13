namespace DTOs.WithoutId;

public class UserPostDTO
{
    public string Name { get; set; }
    public string CINumber { get; set; }
    
    // Contact properties (from FK) 
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Guid> HotelList { get; set; }
}
