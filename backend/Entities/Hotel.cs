namespace Entities;

public class Hotel
{
    public Guid HotelID { get; set; }
    public int Stars { get; set; }
    public string Name { get; set; }
    public bool AllowsPets { get; set; }
    public string Address { get; set; }
    public decimal Tax { get; set; }
    
    // Foreign keys
    public Guid UserID { get; set; }
    public Guid ContactID { get; set; }
    public Guid BathRoomID { get; set; }
}
