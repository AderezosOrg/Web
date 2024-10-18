using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class BathrommDataInjector : DataInjector
{
    public BathrommDataInjector()
    {
        _injectionCommand = @"LOAD DATA INFILE '/var/lib/mysql-files/bathroom.csv'" +  
                        " INTO TABLE Bathroom" +
                        " FIELDS TERMINATED BY ','" +
                        " IGNORE 1 LINES" +
                        " (Id,@Shower,@Toilet,@DressingTable) SET" +
                        " Shower = CAST(@Shower as signed)," +
                        " Toilet = CAST(@Toilet as signed)," +
                        " DressingTable = CAST(@DressingTable as signed);";
    }

}