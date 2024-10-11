using DTOs;

namespace backend.Services.Interfaces;

public interface IBedService
{
    List<BedDTO> GetBeds();
    BedDTO GetBedById(Guid bedID);
    bool CreateBed(BedDTO bedDto);
    BedDTO EditBed(Guid bedID, BedDTO bedDto);
    bool DeleteBed(Guid bedID);
}
