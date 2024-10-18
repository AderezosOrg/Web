using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Db;

namespace backend.Services;

public class HotelService : IHotelService
{
    private IDAO<Hotel> _hotelDao;
    private IDAO<User> _userDao;
    private IDAO<Contact> _contactDao;
    private IDAO<Bathroom> _bathroomDao;
    
    private HotelPostConverter _hotelPostConverter = new HotelPostConverter();
    private HotelConverter _hotelConverter = new HotelConverter();

    public HotelService(IDAO<Hotel> hotelDao, IDAO<User> userDao, IDAO<Contact> contactDao, IDAO<Bathroom> bathroomDao)
    {
        _bathroomDao = bathroomDao;
        _contactDao = contactDao;
        _userDao = userDao;
        _hotelDao = hotelDao;
    }
    public async Task<HotelPostDTO> GetElementById(Guid hotelID)
    {
        await Task.Delay(10);
        var hotel = _hotelDao.Read(hotelID);
        if (hotel == null)
            throw new Exception("Hotel not found");
        var user = _userDao.Read(hotel.UserID);
        var contact = _contactDao.Read(hotel.ContactID);
        var bathroom =_bathroomDao.Read(hotel.BathRoomID);
        return _hotelPostConverter.Convert(hotel, user, contact, bathroom);
    }

    public async Task<List<HotelDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<HotelDTO> result = _hotelDao.ReadAll().Select(h =>
        {
            var user =_userDao.Read(h.UserID);
            var contact = _contactDao.Read(h.ContactID);
            var bathroom = _bathroomDao.Read(h.BathRoomID);
            return _hotelConverter.Convert(h, user, contact, bathroom);
        }).ToList();
        
        return result;
    }

    public async Task<HotelPostDTO> CreateSingleElement(HotelPostDTO hotelPostDto)
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
            _hotelDao.Create(newHotel);
            return hotelPostDto;
        }
        throw new Exception("Hotel not data found");
    }

    public async Task<HotelPostDTO> UpdateElementById(Guid hotelID, HotelPostDTO hotelPostDto)
    {
        await Task.Delay(10);
        var hotel = new Hotel()
        {
            Address = hotelPostDto.Address,
            AllowsPets = hotelPostDto.AllowsPets,
            BathRoomID = Guid.NewGuid(),
            ContactID = Guid.NewGuid(),
            HotelID = hotelID,
            Name = hotelPostDto.Name,
            UserID = Guid.NewGuid(),
        };
        _hotelDao.Update(hotel);
        var user =_userDao.Read(hotel.UserID);
        var contact = _contactDao.Read(hotel.ContactID);
        var bathroom = _bathroomDao.Read(hotel.BathRoomID);
        return _hotelPostConverter.Convert(hotel, user, contact, bathroom);
    }

}
