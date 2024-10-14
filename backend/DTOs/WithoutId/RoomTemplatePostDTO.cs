namespace DTOs.WithId;

public class RoomTemplatePostDTO
{
    public Guid RoomTemplateID { get; set; }
    public string Side { get; set; }
    public int Windows { get; set; }
    public List<BedAddToTemplateDTO> Beds { get; set; }
    public List<BathroomAddToTemplateDTO> Bathrooms { get; set; }
}