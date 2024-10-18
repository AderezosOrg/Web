using System;
using System.Data;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;
using Microsoft.Extensions.Primitives;

namespace Db;

public class RoomTemplateDAO : IDAO<RoomTemplate>
{
    public int Create(RoomTemplate roomTemplate)
    {
        string RoomTemplateID = roomTemplate.RoomTemplateID.ToString();
        string side = roomTemplate.Side.ToString();
        string windows = roomTemplate.Windows.ToString();
        
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder rt = new StringBuilder();
        rt.Append("INSERT INTO RoomTemplate(Id, Side, Windows)")
            .Append("VALUES ('").Append(RoomTemplateID).Append("',' ")
            .Append(side).Append("', ")
            .Append(windows).Append(");");
        
        com.CommandText = rt.ToString();
        return com.ExecuteNonQuery();
    }

    public RoomTemplate? Read(Guid Id)
    {
        string RoomTemplateID = Id.ToString();
        
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder rt = new StringBuilder();
        rt.Append("SELECT * FROM RoomTemplate WHERE Id = ''").Append(RoomTemplateID).AppendLine("';");
        
        com.CommandText = rt.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        reader.Read();

        RoomTemplate toReturn = new RoomTemplate()
        {
            RoomTemplateID = Guid.Parse(reader.GetString(0)),
            Side = reader.GetString(1),
            Windows = reader.GetInt32(2)
        };
        reader.Close();
        return toReturn;
    }

    public List<RoomTemplate>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder rt = new StringBuilder();
        rt.Append("RoomTemplate");
        
        com.CommandText = rt.ToString();
        com.CommandType = CommandType.TableDirect;

        RoomTemplate topAppend;
        
        var reader = com.ExecuteReader();
        List<RoomTemplate> toReturn = new List<RoomTemplate>();
        while (reader.Read())
        {
            topAppend = new RoomTemplate()
            {
                RoomTemplateID = Guid.Parse(reader.GetString(0)),
                Side = reader.GetString(1),
                Windows = reader.GetInt32(2)
            };
            
            toReturn.Add(topAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(RoomTemplate roomTemplate)
    {
        string RoomTemplateID = roomTemplate.RoomTemplateID.ToString();
        string side = roomTemplate.Side.ToString();
        string windows = roomTemplate.Windows.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder rt = new StringBuilder();
        rt.Append("UPDATE RoomTemplate")
            .Append("SET Side = '").Append(side).Append("', ")
            .Append("Windows = ").Append(windows).Append(", ")
            .Append(" WHERE Id = '").Append(RoomTemplateID).Append("';");
        com.CommandText = rt.ToString();
        var reader = com.ExecuteReader();
        int toReturn = reader.RecordsAffected;
        reader.Close();

        return toReturn;
    }

    public bool Delete(Guid Id)
    {
        string RoomTemplateID = Id.ToString();
        
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder rt = new StringBuilder();
        rt.Append("DELETE FROM RoomTemplate")
            .Append(" WHERE Id = '").Append(RoomTemplateID).Append("';");

        com.CommandText = rt.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();
        
        return recordsAffected>0;

    }
}