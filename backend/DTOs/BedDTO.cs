namespace DTOs;

public class BedDTO
{
    public Guid BedID { get; set; }
    public string Size { get; set; }
    public string Capacity { get; set; }
    
    // BedInformation properties (from FK) 
    
    public int BedQuantity { get; set; }
}
