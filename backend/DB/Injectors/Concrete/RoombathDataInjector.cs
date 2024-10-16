using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class RoombathDataInjector : DataInjector
{
    public RoombathDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/roombathInformation.csv'" +  
                        " INTO TABLE RoomBathInformation" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES;";
    }
}