using System.Data;
using System.Text;
using Entities;
using MySql.Data.MySqlClient;

namespace Db;

public class ServiceDAO : IDAO<Service>
{
    
    public int Create(Service h)
    {
        string IdC = h.ServiceID.ToString();
        string type = h.Type;

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Service (Id, Type)")
            .Append("VALUES ('").Append(IdC).Append("','")
                                .Append(type).Append("');");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public Service? Read(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM Service WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) return null;
        reader.Read();
        
        Service toReturn = new Service {
            ServiceID = Guid.Parse(reader.GetString(0)),
            Type = reader.GetString(1)
        };
        reader.Close();
        return toReturn;
    }

    public List<Service>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("Service");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        Service toAppend;

        var reader = com.ExecuteReader();
        List<Service> toReturn = new List<Service>();
        while (reader.Read())
        {
            toAppend = new Service() {
                ServiceID = Guid.Parse(reader.GetString(0)),
                Type = reader.GetString(1)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(Service s)
    {
        string IdC = s.ServiceID.ToString();
        string type = s.Type;

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Service ")
            .Append("SET Type = '").Append(type)
            .Append("' WHERE Id = '").Append(IdC).Append("';");
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
        sb.Append("DELETE FROM Service ")
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