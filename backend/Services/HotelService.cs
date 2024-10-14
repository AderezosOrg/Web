using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class HotelService : AbstractHotelService
{
    private SingletonBD _singletonBD;
    private HotelPostConverter _hotelPostConverter = new HotelPostConverter();
    private HotelConverter _hotelConverter = new HotelConverter();

    public HotelService()
    {
        _singletonBD = SingletonBD.Instance;
    }
    public override async Task<HotelPostDTO> GetHotelById(Guid hotelID)
    {
        await Task.Delay(10);
        var hotel = _singletonBD.GetHotelById(hotelID);
        if (hotel == null)
            throw new Exception("Hotel not found");
        var user = _singletonBD.GetAllUsers().FirstOrDefault(u => u.UserID == hotel.UserID);
        var contact = _singletonBD.GetAllContacts().FirstOrDefault(c => c.ContactID == hotel.ContactID);
        var bathroom = _singletonBD.GetAllBathRooms().FirstOrDefault(b => b.BathRoomID == hotel.BathRoomID);
        
        return _hotelPostConverter.Convert(hotel, user, contact, bathroom);
    }

    public override async Task<List<HotelDTO>> GetHotels()
    {
        await Task.Delay(10);
        List<HotelDTO> result = _singletonBD.GetAllHotels().Select(h =>
        {
            var user = _singletonBD.GetAllUsers().FirstOrDefault(u => u.UserID == h.UserID);
            var contact = _singletonBD.GetAllContacts().FirstOrDefault(c => c.ContactID == h.ContactID);
            var bathroom = _singletonBD.GetAllBathRooms().FirstOrDefault(b => b.BathRoomID == h.BathRoomID);
            return _hotelConverter.Convert(h, user, contact, bathroom);
        }).ToList();
        
        return result;
    }

    public override async Task<HotelPostDTO> CreateHotel(HotelPostDTO hotelPostDto)
    {
        await Task.Delay(10);
        if (hotelPostDto != null)
        {
            var newHotel = new Hotel
            {
                Address = hotelPostDto.Address,
                AllowsPets = hotelPostDto.AllowsPets,
                BathRoomID = Guid.NewGuid(),
                ContactID = Guid.NewGuid(),
                HotelID = Guid.NewGuid(),
                Name = hotelPostDto.Name,
                UserID = Guid.NewGuid(),
            };
            _singletonBD.AddHotel(newHotel);
            
            if (_singletonBD.GetAllHotels().Contains(newHotel))
                return hotelPostDto;
            else
                throw new Exception("Hotel not created");
        }
        throw new Exception("Hotel not data found");
    }

    public override async Task<HotelPostDTO> UpdateHotel(Guid hotelID, HotelPostDTO hotelPostDto)
    {
        await Task.Delay(10);

        var updatedHotel = _singletonBD.UpdateHotel(new Hotel()
        {
            Address = hotelPostDto.Address,
            AllowsPets = hotelPostDto.AllowsPets,
            HotelID = hotelID,
            Name = hotelPostDto.Name,
            Stars = hotelPostDto.Stars
        });

        var user = _singletonBD.GetAllUsers().FirstOrDefault(u => u.UserID == updatedHotel.UserID);
        var contact = _singletonBD.GetAllContacts().FirstOrDefault(c => c.ContactID == updatedHotel.ContactID);
        var bathroom = _singletonBD.GetAllBathRooms().FirstOrDefault(b => b.BathRoomID == updatedHotel.BathRoomID);

        return _hotelPostConverter.Convert(updatedHotel, user, contact, bathroom);
    }

}
