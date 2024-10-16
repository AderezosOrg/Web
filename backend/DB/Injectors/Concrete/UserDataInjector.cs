using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class UserDataInjector : DataInjector
{
    public UserDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/user.csv'" +  
                        " INTO TABLE User" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}