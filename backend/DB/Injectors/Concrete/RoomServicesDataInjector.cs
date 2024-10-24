using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class RoomServicesDataInjector : DataInjector
{
    public RoomServicesDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/roomServices.csv'" +  
                        " INTO TABLE RoomServices" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}