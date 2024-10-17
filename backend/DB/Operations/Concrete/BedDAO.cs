using System.Data;
using System.Text;
using Entities;
using MySql.Data.MySqlClient;

namespace Db;

public class BedDAO : IDAO<Bed>
{
    public int Create(Bed h)
    {
        string IdC = h.BedID.ToString();
        string size = h.Size;
        string capacity = h.Capacity.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Bed (Id, Size, Capacity)")
            .Append("VALUES ('").Append(IdC).Append("',")
                                .Append(size).Append(",")
                                .Append(capacity).Append(");");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public Bed? Read(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM Bed WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) return null;
        reader.Read();
        
        Bed toReturn = new Bed {
            BedID = Guid.Parse(reader.GetString(0)),
            Size = reader.GetString(1),
            Capacity = reader.GetInt32(2)
            
        };
        reader.Close();
        return toReturn;
    }

    public List<Bed>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("Bed");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        Bed toAppend;

        var reader = com.ExecuteReader();
        List<Bed> toReturn = new List<Bed>();
        while (reader.Read())
        {
            toAppend = new Bed {
                BedID = Guid.Parse(reader.GetString(0)),
                Size = reader.GetString(1),
                Capacity = reader.GetInt32(2)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(Bed s)
    {
        string IdC = s.BedID.ToString();
        string size = s.Size;
        string capacity = s.Capacity.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Bed ")
            .Append("SET Size = ").Append(size).Append(", ")
            .Append("Capacity =").Append(capacity)
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
        sb.Append("DELETE FROM Bed ")
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