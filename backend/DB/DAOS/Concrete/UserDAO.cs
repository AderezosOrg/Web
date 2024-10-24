using System.Data;
using System.Text;
using Entities;
using MySql.Data.MySqlClient;

namespace Db;

public class UserDAO : IDAO<User>
{
    
    public int Create(User h)
    {
        string IdC = h.UserID.ToString();
        string name = h.Name;
        string ciNumber = h.CINumber;
        string contactId = h.ContactID.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO User (Id, Name, CINumber, ContactId) ")
            .Append("VALUES ('").Append(IdC).Append("','")
                                .Append(name).Append("','")
                                .Append(ciNumber).Append("','")
                                .Append(contactId).Append("');");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public User? Read(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM User WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) return null;
        reader.Read();
        
        User toReturn = new User {
            UserID = Guid.Parse(reader.GetString(0)),
            Name = reader.GetString(1),
            CINumber = reader.GetString(2),
            ContactID = Guid.Parse(reader.GetString(3))
        };
        reader.Close();
        return toReturn;
    }

    public List<User>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("User");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        User toAppend;

        var reader = com.ExecuteReader();
        List<User> toReturn = new List<User>();
        while (reader.Read())
        {
            toAppend = new User() {
                UserID = Guid.Parse(reader.GetString(0)),
                Name = reader.GetString(1),
                CINumber = reader.GetString(2),
                ContactID = Guid.Parse(reader.GetString(3))
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(User s)
    {
        string IdC = s.UserID.ToString();
        string name = s.Name;
        string ciNumber = s.CINumber;
        string contactId = s.ContactID.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE User ")
            .Append("SET Name = '").Append(name).Append("', ")
            .Append("CINumber = '").Append(ciNumber).Append("', ")
            .Append("ContactId = '").Append(contactId)
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
        sb.Append("DELETE FROM User ")
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