using Entities;

namespace Db;

public interface IBedInformationDAO : ITwoForeignDAO<BedInformation>
{
    List<BedInformation> GetBedInformationsByBedId(Guid bedId);

    List<BedInformation> GetBedInformationByRoomTemplateId(Guid roomTemplateId);

    bool DeleteBedInformationByRoomTemplateId(Guid roomTemplateId);

    bool DeleteBedInformationByBedId(Guid bedId);
}
