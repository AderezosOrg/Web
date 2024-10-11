namespace DTOs;

public class BathroomDTO
{
    public Guid BathRoomID { get; set; }
    public bool Shower { get; set; }
    public bool Toilet { get; set; }
    public bool DressingTable { get; set; }
    
    // RoomBathInformation properties (from FK) 
    
    public int BathroomQuantity { get; set; }
}