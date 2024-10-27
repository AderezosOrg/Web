using backend.MyHappyBD;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using backend.Services.ServicesInterfaces;
using Db;

namespace backend.Services;

public class RoomTemplateService : IRoomTemplateService
{
    private IDAO<RoomTemplate> _roomTemplateDao;
    private IRoombathInformationDAO _roomBathInformationDao;
    private IDAO<Bathroom> _bathroomDao;
    private IBedInformationDAO _bedInformationDao;
    private IDAO<Bed> _bedDAO;
    
    private RoomTemplateConverter _roomTemplateConverter;

    public RoomTemplateService(IDAO<RoomTemplate> roomTemplateDAO, IRoombathInformationDAO roomBathInformationDAO,
        IDAO<Bathroom> bathroomDAO, IBedInformationDAO bedInformationDAO, IDAO<Bed> bedDAO)
    {
        _roomTemplateDao = roomTemplateDAO;
        _roomBathInformationDao = roomBathInformationDAO;
        _bathroomDao = bathroomDAO;
        _bedInformationDao = bedInformationDAO;
        _bedDAO = bedDAO;
        _roomTemplateConverter = new RoomTemplateConverter();
    }

    public async Task<RoomTemplateDTO> GetElementById(Guid roomTemplateId)
    {
        var roomTemplate = _roomTemplateDao.Read(roomTemplateId);
        return _roomTemplateConverter.Convert(
            roomTemplate,
            _roomBathInformationDao.ReadAll(),
            _bathroomDao.ReadAll(),
            _bedInformationDao.ReadAll(),
            _bedDAO.ReadAll());
    }

    public async Task<List<RoomTemplateDTO>> GetAllElements()
    {
        var roomTemplates = _roomTemplateDao.ReadAll();
        List<RoomTemplateDTO> roomTemplateDTOs = roomTemplates.Select(rt =>
        {
            var bathrooms = _bathroomDao.ReadAll();
            var bathroomInformation = _roomBathInformationDao.ReadAll();
            var beds = _bedDAO.ReadAll();
            var bedInformation = _bedInformationDao.ReadAll();
            return _roomTemplateConverter.Convert(rt, bathroomInformation, bathrooms, bedInformation, beds);
        }).ToList();
        return roomTemplateDTOs;
    }

    public async Task<RoomTemplatePostDTO> CreateSingleElement(RoomTemplatePostDTO roomTemplatePostDto)
    {
        var guid = Guid.NewGuid();
        _roomTemplateDao.Create(new RoomTemplate()
        {
            RoomTemplateID = guid,
            Side = roomTemplatePostDto.Side,
            Windows = roomTemplatePostDto.Windows
        });
        foreach (BathroomAddToTemplateDTO bathroomAddToTemplateDto in roomTemplatePostDto.Bathrooms)
        {
            _roomBathInformationDao.Create(new RoomBathInformation()
            {
                BathRoomID = bathroomAddToTemplateDto.BathRoomID,
                Quantity = bathroomAddToTemplateDto.BathroomQuantity,
                RoomTemplateID = guid
            });
        }
        foreach (BedAddToTemplateDTO bedAddToTemplateDto in roomTemplatePostDto.Beds)
        {
            _bedInformationDao.Create(new BedInformation()
            {
                BedID = bedAddToTemplateDto.BedID,
                Quantity = bedAddToTemplateDto.BedQuantity,
                RoomTemplateID = guid
            });
        }

        return roomTemplatePostDto;
    }

    public async Task<RoomTemplatePostDTO> UpdateElementById(Guid roomTemplateId, RoomTemplatePostDTO roomTemplatePostDto)
    {
        _roomTemplateDao.Update(new RoomTemplate()
        {
            RoomTemplateID = roomTemplateId,
            Side = roomTemplatePostDto.Side,
            Windows = roomTemplatePostDto.Windows
        });
        _roomBathInformationDao.DeleteRoomBathInformationByRoomTemplateId(roomTemplateId);
        _bedInformationDao.DeleteBedInformationByRoomTemplateId(roomTemplateId);
        
        foreach (BathroomAddToTemplateDTO bathroomAddToTemplateDto in roomTemplatePostDto.Bathrooms)
        {
            _roomBathInformationDao.Create(new RoomBathInformation()
            {
                BathRoomID = bathroomAddToTemplateDto.BathRoomID,
                Quantity = bathroomAddToTemplateDto.BathroomQuantity,
                RoomTemplateID = roomTemplateId
            });
        }
        foreach (BedAddToTemplateDTO bedAddToTemplateDto in roomTemplatePostDto.Beds)
        {
            _bedInformationDao.Create(new BedInformation()
            {
                BedID = bedAddToTemplateDto.BedID,
                Quantity = bedAddToTemplateDto.BedQuantity,
                RoomTemplateID = roomTemplateId
            });
        }

        return roomTemplatePostDto;
    }
}
