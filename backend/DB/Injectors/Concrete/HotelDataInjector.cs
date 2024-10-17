using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public sealed class HotelDataInjector : DataInjector
{
    public HotelDataInjector()
    {
        _injectionCommand = "LOAD DATA INFILE '/var/lib/mysql-files/hotel.csv'" +  
                        " INTO TABLE Hotel" +
                        " FIELDS TERMINATED BY ','" + 
                        " IGNORE 1 LINES " +
                        " (Id,Stars,Name,@AllowsPets,Address,UserID,ContactID,BathRoomID,Tax) SET" +
                        " AllowsPets = CAST(@AllowsPets as signed);";
    }
}