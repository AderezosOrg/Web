using System;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace Db;

public sealed class RoomBathInformationDAO : IRoombathInformationDAO
{
    public int Create(RoomBathInformation rbi)
    {
        string roomTemplateIdC = rbi.RoomTemplateID.ToString();
        string bathRoomIdC = rbi.BathRoomID.ToString();
        string quantityC = rbi.Quantity.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO RoomBathInformation (RoomTemplateId,BathroomId,Quantity) ")
            .Append("VALUES ('").Append(roomTemplateIdC).Append("','")
                                .Append(bathRoomIdC).Append("', ")
                                .Append(quantityC).Append(");");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public RoomBathInformation? Read(Guid roomTemplateId, Guid bathRoomId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();
        string bathRoomIdC = bathRoomId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM RoomBathInformation WHERE RoomTemplateId = '").Append(roomTemplateIdC).Append("' ")
            .Append("AND BathroomId = '").Append(bathRoomIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }
        reader.Read();
        
        RoomBathInformation toReturn = new RoomBathInformation {
            RoomTemplateID = roomTemplateId,
            BathRoomID = bathRoomId,
            Quantity = Convert.ToInt32(reader.GetInt16(2))
        };

        reader.Close();
        return toReturn;
    }

    public List<RoomBathInformation> ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("RoomBathInformation");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        RoomBathInformation toAppend;

        var reader = com.ExecuteReader();
        List<RoomBathInformation> toReturn = new List<RoomBathInformation>();
        while (reader.Read())
        {
            toAppend = new RoomBathInformation {
                RoomTemplateID = Guid.Parse(reader.GetString(0)),
                BathRoomID = Guid.Parse(reader.GetString(1)),
                Quantity = Convert.ToInt32(reader.GetInt16(2))
            };
            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(RoomBathInformation rbi)
    {
        string roomTemplateIdC = rbi.RoomTemplateID.ToString();
        string bathRoomIdC = rbi.BathRoomID.ToString();
        string quantityC = rbi.Quantity.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE RoomBathInformation ")
            .Append("SET Quantity =").Append(quantityC)
            .Append(" WHERE RoomTemplateId = '").Append(roomTemplateIdC).Append("' ")
            .Append(" AND BathroomId = '").Append(bathRoomIdC).Append("' ;");
        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int toReturn = reader.RecordsAffected;
        reader.Close();
            
        return toReturn;
    }

    public bool Delete(Guid roomTemplateId, Guid bathRoomId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();
        string bathRoomIdC = bathRoomId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM RoomBathInformation ")
            .Append(" WHERE RoomTemplateId = '").Append(roomTemplateIdC).Append("' ")
            .Append("AND BathroomId = '").Append(bathRoomIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }

    public List<RoomBathInformation> GetRoombathInformationsByBathroomId(Guid bathrooomId)
    {
        string bathrooomIdC = bathrooomId.ToString();

        MySqlCommand com = new MySqlCommand("GetBathroomInformationByBathroomId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@bathroomId",bathrooomIdC);
        com.Parameters["@bathroomId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<RoomBathInformation> toReturn = new List<RoomBathInformation>();
        RoomBathInformation toAppend;
        while (reader.Read())
        {
            toAppend = new RoomBathInformation {
                RoomTemplateID = Guid.Parse(reader.GetString(0)),
                BathRoomID = bathrooomId,
                Quantity = reader.GetInt32(1)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }

    public List<RoomBathInformation> GetRoombathInformationsByRoomTemplateId(Guid roomTemplateId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();

        MySqlCommand com = new MySqlCommand("GetBathroomInformationByRoomTemplateId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@roomTemplateId",roomTemplateIdC);
        com.Parameters["@roomTemplateId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<RoomBathInformation> toReturn = new List<RoomBathInformation>();
        RoomBathInformation toAppend;
        while (reader.Read())
        {
            toAppend = new RoomBathInformation {
                RoomTemplateID = roomTemplateId,
                BathRoomID = Guid.Parse(reader.GetString(0)),
                Quantity = reader.GetInt32(1)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }
    
    public bool DeleteRoomBathInformationByRoomTemplateId(Guid roomTemplateId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();

        MySqlCommand com = new MySqlCommand("DeleteRoomBathInformationByRoomTemplateId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@roomTemplateId",roomTemplateIdC);
        com.Parameters["@roomTemplateId"].Direction = ParameterDirection.Input;
        
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }
}
