namespace DTOs.WithId;

public class BedDTO
{
    public Guid BedID { get; set; }
    public string Size { get; set; }
    public int Capacity { get; set; }
    
    // BedInformation properties (from FK) 
    
    public int BedQuantity { get; set; }
}
