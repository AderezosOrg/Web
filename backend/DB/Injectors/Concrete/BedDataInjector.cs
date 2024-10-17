using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class BedDataInjector : DataInjector
{
    public BedDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/bed.csv'" +  
                        " INTO TABLE Bed" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}