using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class RoomTemplateDataInjector : DataInjector
{
    public RoomTemplateDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/roomTemplate.csv'" +  
                        " INTO TABLE RoomTemplate" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}