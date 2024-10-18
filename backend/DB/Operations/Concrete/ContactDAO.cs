using System.Data;
using System.Text;
using Entities;
using MySql.Data.MySqlClient;

namespace Db;

public class ContactDAO : IDAO<Contact>
{
    public int Create(Contact h)
    {
        string IdC = h.ContactID.ToString();
        string phoneNumber = h.PhoneNumber;
        string email = h.Email;

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Contact (Id, PhoneNumber, Email) VALUES ) ")
            .Append("VALUES ('").Append(IdC).Append("','")
                                .Append(phoneNumber).Append("','")
                                .Append(email).Append("');");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public Contact? Read(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM Contact WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) return null;
        reader.Read();
        
        Contact toReturn = new Contact() {
            ContactID = Guid.Parse(reader.GetString(0)),
            PhoneNumber = reader.GetString(1),
            Email = reader.GetString(2)
        };
        reader.Close();
        return toReturn;
    }

    public List<Contact>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("Contact");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        Contact toAppend;

        var reader = com.ExecuteReader();
        List<Contact> toReturn = new List<Contact>();
        while (reader.Read())
        {
            toAppend = new Contact() {
                ContactID = Guid.Parse(reader.GetString(0)),
                PhoneNumber = reader.GetString(1),
                Email = reader.GetString(2)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(Contact s)
    {
        string IdC = s.ContactID.ToString();
        string phoneNumber = s.PhoneNumber;
        string email = s.Email;

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Contact ")
            .Append("SET PhoneNumber = '").Append(phoneNumber).Append("', ")
            .Append("Email ='").Append(email)
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
        sb.Append("DELETE FROM Contact ")
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