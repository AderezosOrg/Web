using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractBedService
{
    public abstract Task<List<BedInfoDTO>> GetBeds();
    public abstract Task<BedPostDTO> GetBedById(Guid bedID);
    public abstract Task<BedPostDTO> CreateBed(BedPostDTO bedDto);
    public abstract Task<BedPostDTO> CreateOnlyBed(BedPostDTO bedDto);
    public abstract Task<BedPostDTO> EditBed(Guid bedID, BedPostDTO bedDto);
    public abstract Task<bool> DeleteBed(Guid bedID);
}