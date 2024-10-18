using System.Data;
using System.Text;
using Entities;
using MySql.Data.MySqlClient;

namespace Db;

public class HotelDAO : IDAO<Hotel>
{
    public int Create(Hotel h)
    {
        string IdC = h.HotelID.ToString();
        string stars = h.Stars.ToString();
        string name = h.Name;
        string allowPets = ObjectMapper.MapBoolean(h.AllowsPets);
        string address = h.Address;
        string tax = h.Tax.ToString();
        string userId = h.UserID.ToString();
        string contactId = h.ContactID.ToString();
        string bathroomId = h.BathRoomID.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Hotel (Id, Stars, Name, AllowPets, Address, Tax, UserID, ContactId, BathroomId)")
            .Append("VALUES ('").Append(IdC).Append("',")
                                .Append(stars).Append(", '")
                                .Append(name).Append("',")
                                .Append(allowPets).Append(", '")
                                .Append(address).Append("',")
                                .Append(tax).Append(", '")
                                .Append(userId).Append("', '")
                                .Append(contactId).Append("', '")
                                .Append(bathroomId).Append("');");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public Hotel? Read(Guid Id)
    {
        string IdC = Id.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM Hotel WHERE Id = '").Append(IdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) return null;
        reader.Read();
        
        Hotel toReturn = new Hotel() {
            HotelID = Guid.Parse(reader.GetString(0)),
            Stars = reader.GetInt16(1),
            Name = reader.GetString(2),
            AllowsPets = reader.GetBoolean(3),
            Address = reader.GetString(4),
            Tax = reader.GetDecimal(5),
            UserID = Guid.Parse(reader.GetString(6)),
            ContactID = Guid.Parse(reader.GetString(7)),
            BathRoomID = Guid.Parse(reader.GetString(8))
        };
        reader.Close();
        return toReturn;
    }

    public List<Hotel>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("Hotel");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        Hotel toAppend;

        var reader = com.ExecuteReader();
        List<Hotel> toReturn = new List<Hotel>();
        while (reader.Read())
        {
            toAppend = new Hotel() {
                HotelID = Guid.Parse(reader.GetString(0)),
                Stars = reader.GetInt16(1),
                Name = reader.GetString(2),
                AllowsPets = reader.GetBoolean(3),
                Address = reader.GetString(4),
                Tax = reader.GetDecimal(5),
                UserID = Guid.Parse(reader.GetString(6)),
                ContactID = Guid.Parse(reader.GetString(7)),
                BathRoomID = Guid.Parse(reader.GetString(8))
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(Hotel h)
    {
        string IdC = h.HotelID.ToString();
        string stars = h.Stars.ToString();
        string name = h.Name;
        string allowPets = ObjectMapper.MapBoolean(h.AllowsPets);
        string address = h.Address;
        string tax = h.Tax.ToString();
        string userId = h.UserID.ToString();
        string contactId = h.ContactID.ToString();
        string bathroomId = h.BathRoomID.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Hotel ")
            .Append("SET Stars = ").Append(stars).Append(", ")
            .Append("Name = '").Append(name).Append("', ")
            .Append("AllowPets =").Append(allowPets).Append(", ")
            .Append("Address ='").Append(address).Append("', ")
            .Append("Tax =").Append(tax).Append(", ")
            .Append("UserId = '").Append(userId).Append("', ")
            .Append("ContactId = '").Append(contactId).Append("', ")
            .Append("BathRoomId = '").Append(bathroomId)
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
        sb.Append("DELETE FROM Hotel ")
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