using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class RoomDataInjector : DataInjector
{
    public RoomDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/room.csv'" +  
                        " INTO TABLE Room" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}