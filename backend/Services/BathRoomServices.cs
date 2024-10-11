using backend.Services.Interfaces;
using DTOs;

namespace backend.Services;

public class BathRoomServices : IBathroomService
{
    private List<BathroomDTO> _bathroom = new List<BathroomDTO>()
    {
        new BathroomDTO()
        {
            BathRoomID = Guid.NewGuid(),
            BathroomQuantity = 2,
            Shower = true,
            Toilet = true,
            DressingTable = true
        },
        new BathroomDTO()
        {
            BathRoomID = Guid.NewGuid(),
            BathroomQuantity = 1,
            Shower = false,
            DressingTable = true,
            Toilet = false
        }
    };

    public List<BathroomDTO> GetBathRooms()
    {
        return _bathroom;
    }

    public BathroomDTO GetBathRoomById(Guid bathroomID)
    {
        return _bathroom.FirstOrDefault(b => b.BathRoomID == bathroomID);
    }

   

    public bool CreateBathRoom(BathroomDTO bathroomDto)
    {
        _bathroom.Add(bathroomDto);
        return true;
    }

    public BathroomDTO EditBathRoom(Guid bathroomID, BathroomDTO bathroomDto)
    {
        var existingBathroom = _bathroom.FirstOrDefault(b => b.BathRoomID == bathroomID);
        if (existingBathroom != null)
        {
            existingBathroom.BathroomQuantity = bathroomDto.BathroomQuantity;
            existingBathroom.Shower = bathroomDto.Shower;
            existingBathroom.DressingTable = bathroomDto.DressingTable;
            existingBathroom.Toilet = bathroomDto.Toilet;
        }
        return existingBathroom;
    }

    public bool DeleteBathRoom(Guid bathroomID)
    {
        var bathroom = _bathroom.FirstOrDefault(b => b.BathRoomID == bathroomID);
        if (bathroom != null)
        {
            _bathroom.Remove(bathroom);
            return true;
        }
        return false;
    }
}
