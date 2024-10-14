using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractRoomTemplateService
{
    public abstract Task<RoomTemplateDTO> GetRoomTemplateById(Guid roomTemplateId);
    public abstract Task<List<RoomTemplateDTO>> GetRoomTemplates();
    public abstract Task<RoomTemplatePostDTO> CreateRoomTemplate(RoomTemplatePostDTO roomTemplatePostDto);
    public abstract Task<RoomTemplatePostDTO> ChangeRoomTemplate(Guid roomTemplateId, RoomTemplatePostDTO roomTemplatePostDto);
    
}