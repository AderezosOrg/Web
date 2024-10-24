using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Db;

namespace backend.Services;

public class BathRoomService : IBathRoomService
{
    private IDAO<Bathroom> _bathroomDao;

    
    private BathroomPostConverter _bathroomPostConverter = new BathroomPostConverter();
    private BathroomConverter _bathroomConverter = new BathroomConverter();
    
    public BathRoomService(IDAO<Bathroom> bathroomDao)
    {
        _bathroomDao = bathroomDao;
    }
    public async Task<List<BathroomInfoDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<BathroomInfoDTO> result = _bathroomDao.ReadAll().Select(b =>
        {
            return _bathroomConverter.Convert(b);
        }).ToList();

        return result;
    }

    public async Task<BathroomPostDTO> GetElementById(Guid bathroomID)
    {
        await Task.Delay(10);
        var bathroom = _bathroomDao.Read(bathroomID);
        if (bathroom == null)
        {
            throw new Exception("Bathroom not found");
        }
        return _bathroomPostConverter.Convert(bathroom);
    }

    public async Task<BathroomPostDTO> CreateSingleElement(BathroomPostDTO bathroomPostDto)
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
            _bathroomDao.Create(newBathroom); 
            return bathroomPostDto;
        }
        throw new Exception("Bathroom not data found");
    }
    
    public async Task<BathroomPostDTO> UpdateElementById(Guid bathroomID, BathroomPostDTO bathroomDto)
    {
        await Task.Delay(100);
        var bathroom = new Bathroom()
        {
            BathRoomID = bathroomID,
            DressingTable = bathroomDto.DressingTable,
            Shower = bathroomDto.Shower,
            Toilet = bathroomDto.Toilet
        };
        _bathroomDao.Update(bathroom);
        return _bathroomPostConverter.Convert(bathroom);
    }
    
    public async Task<bool> DeleteElementById(Guid elementId)
    {
        await Task.Delay(100);
        return _bathroomDao.Delete(elementId);
    }
}
