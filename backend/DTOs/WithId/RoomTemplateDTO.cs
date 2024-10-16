using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace Converters.ToDTO;

public class RoomTemplateDTO
{
    public Guid RoomTemplateID { get; set; }
    public string Side { get; set; }
    public int Windows { get; set; }
    public List<BedDTO> Beds { get; set; }
    public List<BathroomDTO> Bathrooms { get; set; }
}
