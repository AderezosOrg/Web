using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;

namespace backend.Services;

public class BathRoomServices : AbstractBathroomService
{
    private static List<Bathroom> _bathroom = new List<Bathroom>()
    {
        new Bathroom()
        {
            BathRoomID = Guid.NewGuid(),
            Shower = true,
            Toilet = true,
            DressingTable = true
        },
        new Bathroom()
        {
            BathRoomID = Guid.NewGuid(),
            Shower = false,
            DressingTable = true,
            Toilet = false
        }
    };

    private List<RoomBathInformation> _roomBathInformation = new List<RoomBathInformation>()
    {
        new RoomBathInformation()
        {
            BathRoomID = _bathroom[0].BathRoomID, 
            Quantity = 3,
            RoomTemplateID = Guid.NewGuid(),
        },
        new RoomBathInformation()
        {
            BathRoomID = _bathroom[1].BathRoomID,
            Quantity = 2,
            RoomTemplateID = Guid.NewGuid(),
        }
    };
    
    private BathroomPostConverter _bathroomPostConverter = new BathroomPostConverter();
    private BathroomConverter _bathroomConverter = new BathroomConverter();
    
    public override async Task<List<BathroomInfoDTO>> GetBathRooms()
    {
        await Task.Delay(10);
        List<BathroomInfoDTO> result = _bathroom.Select(b =>
        {
            return _bathroomConverter.Convert(b);
        }).ToList();

        return result;
    }

    public override async Task<BathroomPostDTO> GetBathRoomById(Guid bathroomID)
    {
        await Task.Delay(10); 
        var bathroom = _bathroom.FirstOrDefault(b => b.BathRoomID == bathroomID);
        if (bathroom == null)
        {
            throw new Exception("Bathroom not found");
        }
        return _bathroomPostConverter.Convert(bathroom);
    }

    public override async Task<BathroomPostDTO> CreateBathRoom(BathroomPostDTO bathroomPostDto)
    {
        await Task.Delay(100);
        if (bathroomPostDto != null)
        {
            var newBathroom = new Bathroom
            {
                BathRoomID = Guid.NewGuid(),
                Shower = bathroomPostDto.Shower,
                Toilet = bathroomPostDto.Toilet,
                DressingTable = bathroomPostDto.DressingTable
            };
            _bathroom.Add(newBathroom);
            if (_bathroom.Contains(newBathroom))
                return bathroomPostDto;
            else
                throw new Exception("Bathroom not created");
        }
        throw new Exception("Bathroom not data found");
    }
    
    public override async Task<BathroomPostDTO> EditBathRoom(Guid bathroomID, BathroomPostDTO bathroomDto)
    {
        await Task.Delay(100);
        var existingBathroom = _bathroom.FirstOrDefault(b => b.BathRoomID == bathroomID);
        if (existingBathroom != null)
        {
            existingBathroom.Shower = bathroomDto.Shower;
            existingBathroom.DressingTable = bathroomDto.DressingTable;
            existingBathroom.Toilet = bathroomDto.Toilet;
            
            return bathroomDto;
        }
        throw new Exception("Bathroom not found");
    }

    public override async Task<bool> DeleteBathRoom(Guid bathroomID)
    {
        await Task.Delay(100);
        var bathroom = _bathroom.FirstOrDefault(b => b.BathRoomID == bathroomID);
        if (bathroom != null)
        {
            _bathroom.Remove(bathroom);
            if (!_bathroom.Contains(bathroom))
                return true;
            else
                throw new Exception("Bathroom not deleted");
        }
        else 
            throw new Exception("Bathroom not found");
    }
}
