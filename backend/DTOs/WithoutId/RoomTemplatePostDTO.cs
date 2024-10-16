using DTOs.WithId;

namespace DTOs.WithoutId;

public class RoomTemplatePostDTO
{
    public string Side { get; set; }
    public int Windows { get; set; }
    public List<BedAddToTemplateDTO> Beds { get; set; }
    public List<BathroomAddToTemplateDTO> Bathrooms { get; set; }
}
