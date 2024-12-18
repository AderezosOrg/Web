namespace DTOs.WithoutId;

public class RoomNewPostDTO
{
    // Room properties
    public string Code { get; set; }
    public int FloorNumber { get; set; }
    public decimal PricePerNight { get; set; }

    // RoomTemplate properties (from FK)
    public Guid RoomTemplateId { get; set; }

    // Hotel properties (from FK)
    public Guid HotelId { get; set; }
    
    //Room Services
    public List<Guid> RoomServices { get; set; }
}
