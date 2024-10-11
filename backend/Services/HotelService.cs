using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.Interfaces;

namespace backend.Services;

public class HotelService : IHotelService
{
    private List<HotelDTO> _hotels = new List<HotelDTO>
    {
        new HotelDTO
        {
            Address = "123'3'2",
            AllowsPets = false,
            DressingTable = true,
            HotelEmail = "test@testeado.com",
            HotelID = Guid.NewGuid(),
            HotelPhoneNumber = "123123123",
            Name = "My Hotel",
            Shower = false,
            Stars = 3,
            Toilet = false,
            UserEmail = "user@testeado.com",
            UserPhoneNumber = "+23456789",
            UserCINumber = "1272012",
            UserName = "Paco"
        }
    };

    public HotelDTO GetHotelById(Guid hotelID)
    {
        return _hotels.FirstOrDefault(h => h.HotelID == hotelID);
    }

    public List<HotelDTO> GetHotels()
    {
        return _hotels;
    }

    public bool CreateHotel(HotelDTO hotelDto)
    {
        _hotels.Add(hotelDto);
        return true;
    }

    public bool UpdateHotel(Guid hotelID, HotelDTO hotelDto)
    {
        var existingHotel = _hotels.FirstOrDefault(h => h.HotelID == hotelID);
        if (existingHotel != null)
        {
            existingHotel.Address = hotelDto.Address;
            existingHotel.AllowsPets = hotelDto.AllowsPets;
            existingHotel.DressingTable = hotelDto.DressingTable;
            existingHotel.HotelEmail = hotelDto.HotelEmail;
            existingHotel.HotelPhoneNumber = hotelDto.HotelPhoneNumber;
            existingHotel.Name = hotelDto.Name;
            existingHotel.Shower = hotelDto.Shower;
            existingHotel.Stars = hotelDto.Stars;
            existingHotel.Toilet = hotelDto.Toilet;
            existingHotel.UserEmail = hotelDto.UserEmail;
            existingHotel.UserPhoneNumber = hotelDto.UserPhoneNumber;
            existingHotel.UserCINumber = hotelDto.UserCINumber;
            existingHotel.UserName = hotelDto.UserName;
            return true;
        }
        return false;
    }
}
