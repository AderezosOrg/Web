using System;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace Db;

public class BathroomDAO : IDAO<Bathroom>
{
    public int Create(Bathroom b)
    {
        string IdC = b.BathRoomID.ToString();
        string showerC = ObjectMapper.MapBoolean(b.Shower);
        string toiletC = ObjectMapper.MapBoolean(b.Toilet);
        string dressingTableC = ObjectMapper.MapBoolean(b.DressingTable);

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Bathroom (Id,Shower,Toilet,DressingTable)")
            .Append("VALUES ('").Append(IdC).Append("',")
                                .Append(showerC).Append(",")
                                .Append(toiletC).Append(",")
                                .Append(dressingTableC).Append(");");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public Bathroom? Read(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM Bathroom WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) return null;
        reader.Read();
        
        Bathroom toReturn = new Bathroom {
            BathRoomID = Guid.Parse(reader.GetString(0)),
            Shower = reader.GetBoolean(1),
            Toilet = reader.GetBoolean(2),
            DressingTable = reader.GetBoolean(3)
        };
        reader.Close();
        return toReturn;
    }

    public int Update(Bathroom b)
    {
        string IdC = b.BathRoomID.ToString();
        string showerC = ObjectMapper.MapBoolean(b.Shower);
        string toiletC = ObjectMapper.MapBoolean(b.Toilet);
        string dressingTableC = ObjectMapper.MapBoolean(b.DressingTable);

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Bathroom ")
            .Append("SET Shower = ").Append(showerC).Append(", ")
            .Append("Toilet = ").Append(toiletC).Append(", ")
            .Append("DressingTable =").Append(dressingTableC)
            .Append(" WHERE Id = '").Append(IdC).Append("';");
        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int toReturn = reader.RecordsAffected;
        reader.Close();
            
        return toReturn;
    }

    public bool Delete(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM Bathroom ")
            .Append(" WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }
}