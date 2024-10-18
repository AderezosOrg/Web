using Entities;

namespace Db;

public interface IRoomServicesDAO : ITwoForeignDAO<RoomServices>
{
    List<RoomServices> GetRoomServicesByRoomId(Guid roomId);

    List<RoomServices> GetRoomServicesByServiceId(Guid serviceId);
}
