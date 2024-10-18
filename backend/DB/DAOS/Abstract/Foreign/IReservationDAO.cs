using Entities;

namespace Db;

public interface IReservationDAO : ITwoForeignDAO<Reservation>
{
    List<Reservation> GetReservationsByRoomId(Guid roomId);

    List<Reservation> GetReservationsByContactId(Guid contactId);
}
