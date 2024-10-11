namespace DTOs;

public class HotelDTO
{
    // Hotel properties
    public Guid HotelID { get; set; }
    public int Stars { get; set; }
    public string Name { get; set; }
    public bool AllowsPets { get; set; }
    public string Address { get; set; }

    // User properties (from FK)
    public string UserName { get; set; }
    public string UserCINumber { get; set; }
    public string UserPhoneNumber { get; set; }
    public string UserEmail { get; set; }
    
    // Contact properties (from FK) 
    public string HotelPhoneNumber { get; set; }
    public string HotelEmail { get; set; }

    // Bathroom properties (from FK)
    public bool Shower { get; set; }
    public bool Toilet { get; set; }
    public bool DressingTable { get; set; }
}