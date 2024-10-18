using System;
using System.Data;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;
using Microsoft.Extensions.Primitives;
using Mysqlx.Expect;

namespace Db;

public class RoomDAO : IDAO<Room>
{
    public int Create(Room r)
    {
        string IdRoom = r.RoomID.ToString();
        string codeRoom = r.Code;
        string floorNumber = r.FloorNumber.ToString();
        string pricePerNight = r.PricePerNight.ToString();
        string roomTemplateId = r.RoomTemplateID.ToString();
        string hotelId = r.HotelID.ToString();
        
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder a = new StringBuilder();
        a.Append("INSERT INTO Room (Id,Code,FloorNumber,PricePerNight,RoomTemplateId,HotelId)")
            .Append("VALUES ('").Append(IdRoom). Append("','")
                                .Append(codeRoom).Append("',")
                                .Append(floorNumber).Append(",")
                                .Append(pricePerNight).Append(",'")
                                .Append(roomTemplateId).Append("','")
                                .Append(hotelId).Append("');");
        com.CommandText = a.ToString();
        return com.ExecuteNonQuery();
    }

    public Room? Read(Guid Id)
    {
        string IdRoom = Id.ToString();
        
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder a = new StringBuilder();
        a.Append("SELECT * FROM Room WHERE Id = '").Append(IdRoom).Append("';");
        
        com.CommandText = a.ToString();
        var reader = com.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        reader.Read();

        Room toReturn = new Room
        {
            RoomID = Guid.Parse(reader.GetString(0)),
            Code = reader.GetString(1),
            FloorNumber = reader.GetInt32(2),
            PricePerNight = reader.GetDecimal(3),
            RoomTemplateID = Guid.Parse(reader.GetString(4)),
            HotelID = Guid.Parse(reader.GetString(5))
            
        };
        reader.Close();
        return toReturn;
    }

    public List<Room>? ReadAll()
    {
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder a = new StringBuilder();
        a.Append("Room");
        
        com.CommandText = a.ToString();
        com.CommandType = CommandType.TableDirect;

        Room toAppend;
        
        var reader = com.ExecuteReader();
        List<Room> toReturn = new List<Room>();
        while (reader.Read())
        {
            toAppend = new Room
            {
                RoomID = Guid.Parse(reader.GetString(0)),
                Code = reader.GetString(1),
                FloorNumber = reader.GetInt32(2),
                PricePerNight = reader.GetDecimal(3),
                RoomTemplateID = Guid.Parse(reader.GetString(4)),
                HotelID = Guid.Parse(reader.GetString(5))
            };
            
            toReturn.Add(toAppend);
        }
        reader.Close();
        return toReturn;
    }

    public int Update(Room r)
    {
        string IdRoom = r.RoomID.ToString();
        string codeRoom = r.Code;
        string floorNumber = r.FloorNumber.ToString();
        string pricePerNight = r.PricePerNight.ToString();
        string roomTemplateId = r.RoomTemplateID.ToString();
        string hotelId = r.HotelID.ToString();

        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder a = new StringBuilder();
        a.Append("UPDATE Room ")
            .Append("SET Code = '").Append(codeRoom).Append("',")
            .Append("FloorNumber = ").Append(floorNumber).Append(",")
            .Append("PricePerNight = ").Append(pricePerNight).Append(",")
            .Append("RoomTemplateId = '").Append(roomTemplateId).Append("',")
            .Append("HotelId = '").Append(hotelId).Append("',")
            .Append(" WHERE Id = '").Append(IdRoom).Append("';");
        com.CommandText = a.ToString();
        var reader = com.ExecuteReader();
        int toReturn = reader.RecordsAffected;
        reader.Close();

        return toReturn;
    }

    public bool Delete(Guid Id)
    {
        string IdRoom = Id.ToString();
        
        MySqlCommand com = new MySqlCommand();
        com.Connection = DbUtils.GetConnection();
        
        StringBuilder a = new StringBuilder();
        a.Append("DELETE FROM Room")
            .Append("WHERE Id = '").Append(IdRoom).Append("';");
        
        com.CommandText = a.ToString();
        var reader = com.ExecuteReader();
        int recordsAffected;
        
        reader.Read();
        recordsAffected = reader.RecordsAffected;
        reader.Close();

        return recordsAffected>0;
    }
    
}