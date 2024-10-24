using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class BedInformationDataInjector : DataInjector
{
    public BedInformationDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/bedInformation.csv'" +  
                        " INTO TABLE BedInformation" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES;";
    }
}