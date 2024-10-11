namespace DTOs;

public class RoomDTO
{
    // Room properties
    public Guid RoomID { get; set; }
    public string Code { get; set; }
    public int FloorNumber { get; set; }
    public bool Available { get; set; }

    // RoomTemplate properties (from FK)
    public string RoomTemplateSide { get; set; }
    public int RoomTemplateWindows { get; set; }
    
    public List<Guid> Beds { get; set; }
    public List<Guid> Bathrooms { get; set; }

    // Hotel properties (from FK)
    public string HotelName { get; set; }
    public bool HotelAllowsPets { get; set; } 
}
