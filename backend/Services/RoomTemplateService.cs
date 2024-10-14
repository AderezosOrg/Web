using backend.MyHappyBD;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services.AbstractClass;

public class RoomTemplateService : AbstractRoomTemplateService
{
    private SingletonBD _singletonBd;
    private RoomTemplateConverter _roomTemplateConverter;

    public RoomTemplateService()
    {
        _singletonBd = SingletonBD.Instance;
        _roomTemplateConverter = new RoomTemplateConverter();
    }

    public override async Task<RoomTemplateDTO> GetRoomTemplateById(Guid roomTemplateId)
    {
        var roomTemplate = _singletonBd.GetRoomTemplateById(roomTemplateId);
        return _roomTemplateConverter.Convert(
            roomTemplate,
            _singletonBd.GetAllBathroomInformation(),
            _singletonBd.GetAllBathRooms(),
            _singletonBd.GetAllBedInformation(),
            _singletonBd.GetAllBeds());
    }

    public override async Task<List<RoomTemplateDTO>> GetRoomTemplates()
    {
        var roomTemplates = _singletonBd.GetAllRoomTemplates();
        List<RoomTemplateDTO> roomTemplateDTOs = roomTemplates.Select(rt =>
        {
            var bathrooms = _singletonBd.GetAllBathRooms();
            var bathroomInformation = _singletonBd.GetAllBathroomInformation();
            var beds = _singletonBd.GetAllBeds();
            var bedInformation = _singletonBd.GetAllBedInformation();
            return _roomTemplateConverter.Convert(rt, bathroomInformation, bathrooms, bedInformation, beds);
        }).ToList();
        return roomTemplateDTOs;
    }

    public override async Task<RoomTemplatePostDTO> CreateRoomTemplate(RoomTemplatePostDTO roomTemplatePostDto)
    {
        var guid = Guid.NewGuid();
        _singletonBd.AddRoomTemplate(new RoomTemplate()
        {
            RoomTemplateID = guid,
            Side = roomTemplatePostDto.Side,
            Windows = roomTemplatePostDto.Windows
        });
        foreach (BathroomAddToTemplateDTO bathroomAddToTemplateDto in roomTemplatePostDto.Bathrooms)
        {
            _singletonBd.AddBathroomInformation(new RoomBathInformation()
            {
                BathRoomID = bathroomAddToTemplateDto.BathRoomID,
                Quantity = bathroomAddToTemplateDto.BathroomQuantity,
                RoomTemplateID = guid
            });
        }
        foreach (BedAddToTemplateDTO bedAddToTemplateDto in roomTemplatePostDto.Beds)
        {
            _singletonBd.AddBedInformation(new BedInformation()
            {
                BedID = bedAddToTemplateDto.BedID,
                Quantity = bedAddToTemplateDto.BedQuantity,
                RoomTemplateID = guid
            });
        }

        return roomTemplatePostDto;
    }

    public override async Task<RoomTemplatePostDTO> ChangeRoomTemplate(Guid roomTemplateId, RoomTemplatePostDTO roomTemplatePostDto)
    {
        _singletonBd.UpdateRoomTemplate(new RoomTemplate()
        {
            RoomTemplateID = roomTemplateId,
            Side = roomTemplatePostDto.Side,
            Windows = roomTemplatePostDto.Windows
        });
        _singletonBd.DeleteBathroomInformationByRoomTemplateId(roomTemplateId);
        _singletonBd.DeleteBedByRoomTemplateIdInformation(roomTemplateId);
        
        foreach (BathroomAddToTemplateDTO bathroomAddToTemplateDto in roomTemplatePostDto.Bathrooms)
        {
            _singletonBd.AddBathroomInformation(new RoomBathInformation()
            {
                BathRoomID = bathroomAddToTemplateDto.BathRoomID,
                Quantity = bathroomAddToTemplateDto.BathroomQuantity,
                RoomTemplateID = roomTemplateId
            });
        }
        foreach (BedAddToTemplateDTO bedAddToTemplateDto in roomTemplatePostDto.Beds)
        {
            _singletonBd.AddBedInformation(new BedInformation()
            {
                BedID = bedAddToTemplateDto.BedID,
                Quantity = bedAddToTemplateDto.BedQuantity,
                RoomTemplateID = roomTemplateId
            });
        }

        return roomTemplatePostDto;
    }
}