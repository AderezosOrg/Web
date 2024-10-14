using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class BathRoomServices : AbstractBathroomService
{
    private SingletonBD _singletonBd;

    
    private BathroomPostConverter _bathroomPostConverter = new BathroomPostConverter();
    private BathroomConverter _bathroomConverter = new BathroomConverter();
    
    public BathRoomServices()
    {
        _singletonBd = SingletonBD.Instance;
    }
    public override async Task<List<BathroomInfoDTO>> GetBathRooms()
    {
        await Task.Delay(10);
        List<BathroomInfoDTO> result = _singletonBd.GetAllBathRooms().Select(b =>
        {
            return _bathroomConverter.Convert(b);
        }).ToList();

        return result;
    }

    public override async Task<BathroomPostDTO> GetBathRoomById(Guid bathroomID)
    {
        await Task.Delay(10);
        var bathroom = _singletonBd.GetBathRoomById(bathroomID);
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
            _singletonBd.AddBathroom(newBathroom);
            if (_singletonBd.GetAllBathRooms().Contains(newBathroom))
                return bathroomPostDto;
            else
                throw new Exception("Bathroom not created");
        }
        throw new Exception("Bathroom not data found");
    }
    
    public override async Task<BathroomPostDTO> EditBathRoom(Guid bathroomID, BathroomPostDTO bathroomDto)
    {
        await Task.Delay(100);
        return _bathroomPostConverter.Convert(_singletonBd.UpdateBathroom(new Bathroom()
        {
            BathRoomID = bathroomID,
            DressingTable = bathroomDto.DressingTable,
            Shower = bathroomDto.Shower,
            Toilet = bathroomDto.Toilet
        }));
    }

    public override async Task<bool> DeleteBathRoom(Guid bathroomID)
    {
        await Task.Delay(100);
        return _singletonBd.DeleteBathroom(bathroomID);
    }
}
