namespace DTOs.WithoutId;

public class BedPostDTO
{
    public string Size { get; set; }
    public string Capacity { get; set; }
    
    // BedInformation properties (from FK) 
    
    public int BedQuantity { get; set; }
}
