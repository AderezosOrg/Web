using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class ContactDataInjector : DataInjector
{
    public ContactDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/contact.csv'" +  
                        " INTO TABLE Contact" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES";
    }
}