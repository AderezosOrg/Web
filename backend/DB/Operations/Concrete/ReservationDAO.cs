using System;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace Db;

public sealed class ReservationDAO : ITwoForeignDAO<Reservation>
{
    public int Create(Reservation r)
    {
        string ContactIdC = r.ContactID.ToString();
        string RoomIdC = r.RoomID.ToString();
        string ReservationDateC = ObjectMapper.MapDateTime(r.ReservationDate);
        string UseDateC = ObjectMapper.MapDateTime(r.UseDate);
        string CancelledC = ObjectMapper.MapBoolean(r.Cancelled);

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Reservation (ContactId,RoomId,ReservationDate,UseDate,Cancelled)")
            .Append("VALUES ('").Append(ContactIdC).Append("','")
                                .Append(RoomIdC).Append("','")
                                .Append(ReservationDateC).Append("','")
                                .Append(UseDateC).Append("',")
                                .Append(CancelledC).Append(");");

        com.CommandText = sb.ToString();
        return com.ExecuteNonQuery();
    }

    public Reservation? Read(Guid contactId, Guid roomId)
    {
        string contactIdC = contactId.ToString();
        string roomIdC = roomId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM Reservation WHERE ContactId = '").Append(contactIdC).Append("' ")
            .Append("AND RoomId = '").Append(roomIdC).Append("';");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }
        reader.Read();
        
        Reservation toReturn = new Reservation {
            ContactID = Guid.Parse(reader.GetString(0)),
            RoomID = Guid.Parse(reader.GetString(1)),
            ReservationDate = reader.GetDateTime(2),
            UseDate = reader.GetDateTime(3),
            Cancelled = reader.GetBoolean(4)
        };
        reader.Close();
        return toReturn;
    }

    public List<Reservation> ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("Reservation");

        com.CommandText = sb.ToString();
        com.CommandType = CommandType.TableDirect;

        Reservation toAppend;

        var reader = com.ExecuteReader();
        List<Reservation> toReturn = new List<Reservation>();
        while (reader.Read())
        {
            toAppend = new Reservation {
                ContactID = Guid.Parse(reader.GetString(0)),
                RoomID = Guid.Parse(reader.GetString(1)),
                ReservationDate = reader.GetDateTime(2),
                UseDate = reader.GetDateTime(3),
                Cancelled = reader.GetBoolean(4)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(Reservation r)
    {
        string ContactIdC = r.ContactID.ToString();
        string RoomIdC = r.RoomID.ToString();
        string ReservationDateC = ObjectMapper.MapDateTime(r.ReservationDate);
        string UseDateC = ObjectMapper.MapDateTime(r.UseDate);
        string CancelledC = ObjectMapper.MapBoolean(r.Cancelled);

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Reservation ")
            .Append("SET ReservationDate = '").Append(ReservationDateC).Append("', ")
            .Append("UseDate = '").Append(UseDateC).Append("', ")
            .Append("Cancelled =").Append(CancelledC)
            .Append(" WHERE ContactId = '").Append(ContactIdC).Append("' ")
            .Append(" AND RoomId = '").Append(RoomIdC).Append("' ;");
        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int toReturn = reader.RecordsAffected;
        reader.Close();
            
        return toReturn;
    }

    public bool Delete(Guid contactId, Guid roomId)
    {
        string IdContactC = contactId.ToString();
        string IdRoomC = roomId.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();

        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM Reservation ")
            .Append(" WHERE ContactId = '").Append(IdContactC).Append("' ")
            .Append("AND RoomId = '").Append(IdRoomC).Append("' ;");

        com.CommandText = sb.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;

        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }

    public List<Reservation> GetReservationsByRoomId(Guid roomId)
    {
        string roomIdC = roomId.ToString();

        MySqlCommand com = new MySqlCommand("GetReservationByRoomId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@roomId",roomIdC);
        com.Parameters["@roomId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<Reservation> toReturn = new List<Reservation>();
        Reservation toAppend;
        while (reader.Read())
        {
            toAppend = new Reservation {
                ContactID = Guid.Parse(reader.GetString(0)),
                RoomID = roomId,
                ReservationDate = reader.GetDateTime(1),
                UseDate = reader.GetDateTime(2),
                Cancelled = reader.GetBoolean(3)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }

    public List<Reservation> GetReservationsByContactId(Guid contactId)
    {
        string contactIdC = contactId.ToString();

        MySqlCommand com = new MySqlCommand("GetReservationByContactId",DbUtils.GetConnection());
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@contactId",contactIdC);
        com.Parameters["@contactId"].Direction = ParameterDirection.Input;

        var reader = com.ExecuteReader();
        if (!reader.HasRows) 
        {
            reader.Close();
            return null;
        }

        List<Reservation> toReturn = new List<Reservation>();
        Reservation toAppend;
        while (reader.Read())
        {
            toAppend = new Reservation {
                ContactID = contactId,
                RoomID = Guid.Parse(reader.GetString(0)),
                ReservationDate = reader.GetDateTime(1),
                UseDate = reader.GetDateTime(2),
                Cancelled = reader.GetBoolean(3)
            };

            toReturn.Add(toAppend);
        }
        reader.Close();
        
        return toReturn;
    }
}