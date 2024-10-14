using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace Converters.ToDTO;

public class RoomTemplateDTO
{
    public Guid RoomTemplateID { get; set; }
    public string Side { get; set; }
    public int Windows { get; set; }
    public List<BathroomPostDTO> Beds { get; set; }
    public List<BedDTO> Bathrooms { get; set; }
}