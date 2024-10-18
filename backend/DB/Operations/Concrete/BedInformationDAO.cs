using System;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace Db;

public sealed class BedInformationDAO : ITwoForeignDAO<BedInformation>
{
    public int Create(BedInformation bi)
    {
        string roomTemplateIdC = bi.RoomTemplateID.ToString();
        string bedIdC = bi.BedID.ToString();
        string quantityC = bi.Quantity.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO BedInformation (RoomTemplateID,BedID,Quantity) ")
            .Append("VALUES ('").Append(roomTemplateIdC).Append("','")
                                .Append(bedIdC).Append("', ")
                                .Append(quantityC).Append(");");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public BedInformation? Read(Guid roomTemplateId, Guid bedId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();
        string bedIdC = bedId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM BedInformation WHERE RoomTemplateId = '").Append(roomTemplateIdC).Append("' ")
            .Append("AND BedId = '").Append(bedIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }
        reader.Read();
        
        BedInformation toReturn = new BedInformation {
            RoomTemplateID = roomTemplateId,
            BedID = bedId,
            Quantity = Convert.ToInt32(reader.GetInt16(2))
        };

        reader.Close();
        return toReturn;
    }

    public List<BedInformation> ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("BedInformation");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        BedInformation toAppend;

        var reader = com.ExecuteReader();
        List<BedInformation> toReturn = new List<BedInformation>();
        while (reader.Read())
        {
            toAppend = new BedInformation {
                RoomTemplateID = Guid.Parse(reader.GetString(0)),
                BedID = Guid.Parse(reader.GetString(1)),
                Quantity = Convert.ToInt32(reader.GetInt16(2))
            };
            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(BedInformation bi)
    {
        string roomTemplateIdC = bi.RoomTemplateID.ToString();
        string bedIdC = bi.BedID.ToString();
        string quantityC = bi.Quantity.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE BedInformation ")
            .Append("SET Quantity =").Append(quantityC)
            .Append(" WHERE RoomTemplateId = '").Append(roomTemplateIdC).Append("' ")
            .Append(" AND BedId = '").Append(bedIdC).Append("' ;");
        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int toReturn = reader.RecordsAffected;
        reader.Close();
            
        return toReturn;
    }

    public bool Delete(Guid roomTemplateId, Guid bedId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();
        string bedIdC = bedId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM BedInformation ")
            .Append(" WHERE RoomTemplateId = '").Append(roomTemplateIdC).Append("' ")
            .Append("AND BedId = '").Append(bedIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }

    public List<BedInformation> GetBedInformationsByBedId(Guid bedId)
    {
        string bedIdC = bedId.ToString();

        MySqlCommand com = new MySqlCommand("GetBedInformationByBedId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@bedId",bedIdC);
        com.Parameters["@bedId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<BedInformation> toReturn = new List<BedInformation>();
        BedInformation toAppend;
        while (reader.Read())
        {
            toAppend = new BedInformation {
                RoomTemplateID = Guid.Parse(reader.GetString(0)),
                BedID = bedId,
                Quantity = reader.GetInt32(1)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }

    public List<BedInformation> GetBedInformationByRoomTemplateId(Guid roomTemplateId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();

        MySqlCommand com = new MySqlCommand("GetBedInformationByRoomTemplateId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@roomTemplateId",roomTemplateIdC);
        com.Parameters["@roomTemplateId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<BedInformation> toReturn = new List<BedInformation>();
        BedInformation toAppend;
        while (reader.Read())
        {
            toAppend = new BedInformation {
                RoomTemplateID = roomTemplateId,
                BedID = Guid.Parse(reader.GetString(0)),
                Quantity = reader.GetInt32(1)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }

    public bool DeleteBedInformationByRoomTemplateId(Guid roomTemplateId)
    {
        string roomTemplateIdC = roomTemplateId.ToString();

        MySqlCommand com = new MySqlCommand("DeleteBedInformationByRoomTemplateId",DbUtils.GetConnection());
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

    public bool DeleteBedInformationByBedId(Guid bedId)
    {
        string bedIdC = bedId.ToString();

        MySqlCommand com = new MySqlCommand("DeleteBedInformationByBedId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@bedId",bedIdC);
        com.Parameters["@bedId"].Direction = ParameterDirection.Input;
        
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }
}