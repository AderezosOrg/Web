using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class ServiceDataInjector : DataInjector
{
    public ServiceDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/service.csv'" +  
                        " INTO TABLE Service" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}