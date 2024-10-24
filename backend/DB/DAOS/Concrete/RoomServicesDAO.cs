using System;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace Db;

public sealed class RoomServicesDAO : IRoomServicesDAO
{
    public int Create(RoomServices rs)
    {
        string RoomIdC = rs.RoomID.ToString();
        string ServiceIdC = rs.ServiceID.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO RoomServices (RoomId,ServiceId)")
            .Append("VALUES ('").Append(RoomIdC).Append("','")
                                .Append(ServiceIdC).Append("');");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public RoomServices? Read(Guid roomId,Guid serviceId)
    {
        string roomIdC = roomId.ToString();
        string serviceIdC = serviceId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM RoomServices WHERE RoomId = '").Append(roomIdC).Append("' ")
            .Append("AND ServiceId = '").Append(serviceIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }
        reader.Read();
        
        RoomServices toReturn = new RoomServices {
            RoomID = roomId,
            ServiceID = serviceId
        };

        reader.Close();
        return toReturn;
    }

    public List<RoomServices> ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("RoomServices");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        RoomServices toAppend;

        var reader = com.ExecuteReader();
        List<RoomServices> toReturn = new List<RoomServices>();
        while (reader.Read())
        {
            toAppend = new RoomServices {
                RoomID = Guid.Parse(reader.GetString(0)),
                ServiceID = Guid.Parse(reader.GetString(1))
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(RoomServices rs)
    {
        return 0;
    }

    public bool Delete(Guid roomId, Guid serviceId)
    {
        string roomIdC = roomId.ToString();
        string serviceIdC = serviceId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM RoomServices ")
            .Append(" WHERE RoomId = '").Append(roomIdC).Append("' ")
            .Append("AND ServiceId = '").Append(serviceIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }

    public List<RoomServices> GetRoomServicesByRoomId(Guid roomId)
    {
        string roomIdC = roomId.ToString();

        MySqlCommand com = new MySqlCommand("GetRoomServicesByRoomId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@roomId",roomIdC);
        com.Parameters["@roomId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<RoomServices> toReturn = new List<RoomServices>();
        RoomServices toAppend;
        while (reader.Read())
        {
            toAppend = new RoomServices {
                RoomID = roomId,
                ServiceID = Guid.Parse(reader.GetString(0))
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }

    public List<RoomServices> GetRoomServicesByServiceId(Guid serviceId)
    {
        string serviceIdC = serviceId.ToString();

        MySqlCommand com = new MySqlCommand("GetRoomServicesByServiceId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@serviceId",serviceIdC);
        com.Parameters["@serviceId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<RoomServices> toReturn = new List<RoomServices>();
        RoomServices toAppend;
        while (reader.Read())
        {
            toAppend = new RoomServices {
                RoomID = Guid.Parse(reader.GetString(0)),
                ServiceID = serviceId
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }
}