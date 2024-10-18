using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class ReservationDataInjector : DataInjector
{
    public ReservationDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/reservation.csv'" +  
                        " INTO TABLE Reservation" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES" +
                        " (ContactID,RoomID,@ReservationDate,@UseDate,@Cancelled) SET" +
                        " ReservationDate = CAST(@ReservationDate as DATETIME), " +
                        " UseDate = CAST(@ReservationDate as DATETIME), " +
                        " Cancelled = CAST(@Cancelled as signed);";
    }
}