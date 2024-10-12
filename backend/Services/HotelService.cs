using DTOs;
using System.Collections.Generic;
using System.Linq;
using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;

namespace backend.Services;

public class HotelService : AbstractHotelService
{
    private static List<Hotel> _hotels = new List<Hotel>()
    {
        new Hotel()
        {
            Address = "123'3'2",
            AllowsPets = false,
            HotelID = Guid.NewGuid(),
            Name = "My Hotel",
            Stars = 3,
        },
        new Hotel()
        {
            Address = "236'4'0",
            AllowsPets = true,
            HotelID = Guid.NewGuid(),
            Name = "YOUR Hotel",
            Stars = 5,
        }
    };

    private List<User> _users = new List<User>()
    {
        new User()
        {
            CINumber = "123456",
            ContactID = Guid.NewGuid(),
            Name = "John Doe",
            UserID = Guid.NewGuid(),
        },
        new User()
        {
            CINumber = "654321",
            ContactID = Guid.NewGuid(),
            Name = "DOE hSA",
            UserID = Guid.NewGuid(),
        }
    };

    private List<Contact> _contacts = new List<Contact>()
    {
        new Contact()
        {
            ContactID = Guid.NewGuid(),
            Email = "johndoe@gmail.com",
            PhoneNumber = "555-555-5555",
        },
        new Contact()
        {
            ContactID = Guid.NewGuid(),
            Email = "seguro@gmail.com",
            PhoneNumber = "666-666-5555",
        }
    };

    private List<Bathroom> _bathrooms = new List<Bathroom>()
    {
        new Bathroom()
        {
            BathRoomID = Guid.NewGuid(),
            DressingTable = true,
            Shower = false,
            Toilet = true
        },
        new Bathroom()
        {
            BathRoomID = Guid.NewGuid(),
            DressingTable = false,
            Shower = false,
            Toilet = true
        }
    };
    
    private HotelPostConverter _hotelPostConverter = new HotelPostConverter();
    private HotelConverter _hotelConverter = new HotelConverter();
    
    public override async Task<HotelPostDTO> GetHotelById(Guid hotelID)
    {
        await Task.Delay(10);
        var hotel = _hotels.FirstOrDefault(h => h.HotelID == hotelID);
        if (hotel == null)
            throw new Exception("Hotel not found");
        var user = _users.FirstOrDefault(u => u.UserID == hotel.UserID);
        var contact = _contacts.FirstOrDefault(c => c.ContactID == hotel.ContactID);
        var bathroom = _bathrooms.FirstOrDefault(b => b.BathRoomID == hotel.BathRoomID);
        return _hotelPostConverter.Convert(hotel, user, contact, bathroom);
    }

    public override async Task<List<HotelDTO>> GetHotels()
    {
        await Task.Delay(10);
        List<HotelDTO> result = _hotels.Select(h =>
        {
            var user = _users.FirstOrDefault(u => u.UserID == h.UserID);
            var contact = _contacts.FirstOrDefault(c => c.ContactID == h.ContactID);
            var bathroom = _bathrooms.FirstOrDefault(b => b.BathRoomID == h.BathRoomID);
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
            _hotels.Add(newHotel);

            var newUser = new User
            {
                CINumber = hotelPostDto.UserCINumber,
                ContactID = Guid.NewGuid(),
                Name = hotelPostDto.UserName,
                UserID = Guid.NewGuid(),
            };
            _users.Add(newUser);

            var newContact = new Contact
            {
                ContactID = Guid.NewGuid(),
                Email = hotelPostDto.HotelEmail,
                PhoneNumber = hotelPostDto.HotelPhoneNumber
            };
            _contacts.Add(newContact);

            var newBathroom = new Bathroom
            {
                BathRoomID = Guid.NewGuid(),
                DressingTable = hotelPostDto.DressingTable,
                Shower = hotelPostDto.Shower,
                Toilet = hotelPostDto.Toilet
            };
            _bathrooms.Add(newBathroom);
            if (_hotels.Contains(newHotel) && _users.Contains(newUser) && _contacts.Contains(newContact) && _bathrooms.Contains(newBathroom))
                return hotelPostDto;
            else
                throw new Exception("Hotel not created");
        }
        throw new Exception("Hotel not data found");
    }

    public override async Task<HotelPostDTO> UpdateHotel(Guid hotelID, HotelPostDTO hotelPostDtoDto)
    {
        await Task.Delay(10);
        var existingHotel = _hotels.FirstOrDefault(h => h.HotelID == hotelID);
        if (existingHotel != null)
        {
            existingHotel.Name = hotelPostDtoDto.Name;
            existingHotel.Address = hotelPostDtoDto.Address;
            existingHotel.AllowsPets = hotelPostDtoDto.AllowsPets;
            existingHotel.Stars = hotelPostDtoDto.Stars;
            
            return hotelPostDtoDto;
        }
        throw new Exception("Hotel not found");
    }
}
