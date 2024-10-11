namespace backend.Services.Interfaces;
using DTOs;

public interface IHotelService
{
    HotelDTO GetHotelById(Guid hotelID);
    List<HotelDTO> GetHotels();
    bool CreateHotel(HotelDTO hotelDto);
    bool UpdateHotel(Guid hotelID, HotelDTO hotelDto);
}
