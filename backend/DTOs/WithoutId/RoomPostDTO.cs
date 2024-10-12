namespace DTOs.WithoutId;

public class RoomPostDTO
{
    // Room properties
    public string Code { get; set; }
    public int FloorNumber { get; set; }
    public decimal PricePerNight { get; set; }

    // RoomTemplate properties (from FK)
    public string RoomTemplateSide { get; set; }
    public int RoomTemplateWindows { get; set; }
    
    public List<Guid> Beds { get; set; }
    public List<Guid> Bathrooms { get; set; }
    public List<Guid> Services { get; set; }

    // Hotel properties (from FK)
    public string HotelName { get; set; }
    public bool HotelAllowsPets { get; set; } 
}
