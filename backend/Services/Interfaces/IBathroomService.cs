namespace backend.Services.Interfaces;
using DTOs;

public interface IBathroomService
{
    List<BathroomDTO> GetBathRooms();
    BathroomDTO GetBathRoomById(Guid bathroomID);
    bool CreateBathRoom(BathroomDTO bathroomDto);
    BathroomDTO EditBathRoom(Guid bathroomID, BathroomDTO bathroomDto);
    bool DeleteBathRoom(Guid bathroomID);
}
