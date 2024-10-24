using System;
using System.Data;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public static class DbUtils {

    private static MySqlConnection _conn;

    private static string GetConnectionString(){
        var config =
        new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("dbsettings.json", true)
        .Build();
        return config["DbSettings:ConnectionUrl"];
    }

    public static MySqlConnection GetConnection(){
        return _conn;
    }

    public static void OpenConnection(){
        _conn = new MySqlConnection(GetConnectionString());
        _conn.Open();
    }

    public static void TruncateAllTables(){
        MySqlCommand com = new MySqlCommand("TruncateAllTables", _conn);
        com.CommandType = CommandType.StoredProcedure;
        com.ExecuteNonQuery();
    }

    public static void InjectData(){
        IDataInjector injector = new BathrommDataInjector();
        injector.InjectData(_conn);
     
        injector = new BedDataInjector();
        injector.InjectData(_conn);

        injector = new ContactDataInjector();
        injector.InjectData(_conn);

        injector = new ServiceDataInjector();
        injector.InjectData(_conn);

        injector = new RoomTemplateDataInjector();
        injector.InjectData(_conn);

        injector = new UserDataInjector();
        injector.InjectData(_conn);

        injector = new HotelDataInjector();
        injector.InjectData(_conn);

        injector = new RoomDataInjector();
        injector.InjectData(_conn);

        injector = new RoomServicesDataInjector();
        injector.InjectData(_conn);

        injector = new ReservationDataInjector();
        injector.InjectData(_conn);

        injector = new BedInformationDataInjector();
        injector.InjectData(_conn);

        injector = new RoombathDataInjector();
        injector.InjectData(_conn);
    }
}