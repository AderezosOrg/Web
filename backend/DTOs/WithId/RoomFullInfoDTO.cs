using DTOs.WithoutId;

namespace DTOs.WithId;

public class RoomFullInfoDTO
{
    // Room properties
    public Guid RoomID { get; set; }
    public string Code { get; set; }
    public int FloorNumber { get; set; }
    public decimal PricePerNight { get; set; }

    // RoomTemplate properties (from FK)
    public string RoomTemplateSide { get; set; }
    public int RoomTemplateWindows { get; set; }
    
    public List<BedPostDTO> Beds { get; set; }
    public List<BathroomPostDTO> Bathrooms { get; set; }
    public List<ServicePostDTO> Services { get; set; }

    // Hotel properties (from FK)
    public string HotelName { get; set; }
    public bool HotelAllowsPets { get; set; }
}
