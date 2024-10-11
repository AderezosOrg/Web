namespace Entities;

public class Room
{
    public Guid RoomID { get; set; }
    public string Code { get; set; }
    public int FloorNumber { get; set; }
    public bool Available { get; set; }
    public uint PricePerNight { get; set; }

    // Foreign keys
    public Guid RoomTemplateID { get; set; }
    public Guid HotelID { get; set; }
}
