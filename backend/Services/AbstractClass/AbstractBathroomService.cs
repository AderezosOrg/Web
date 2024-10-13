using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractBathroomService
{
    public abstract Task<List<BathroomInfoDTO>> GetBathRooms();
    public abstract Task<BathroomPostDTO> GetBathRoomById(Guid bathroomID);
    public abstract Task<BathroomPostDTO> CreateBathRoom(BathroomPostDTO bathroomDto);
    public abstract Task<BathroomPostDTO> EditBathRoom(Guid bathroomID, BathroomPostDTO bathroomDto);
    public abstract Task<bool> DeleteBathRoom(Guid bathroomID);
}