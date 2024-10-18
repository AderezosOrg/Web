using Entities;

namespace Db;

public interface IRoombathInformationDAO : ITwoForeignDAO<RoomBathInformation>
{
    List<RoomBathInformation> GetRoombathInformationsByBathroomId(Guid bathrooomId);

    List<RoomBathInformation> GetRoombathInformationsByRoomTemplateId(Guid roomTemplateId);

    bool DeleteRoomBathInformationByRoomTemplateId(Guid roomTemplateId);
}
