using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractHotelService
{
    public abstract Task<HotelPostDTO> GetHotelById(Guid hotelID);
    public abstract Task<List<HotelPostDTO>> GetHotels();
    public abstract Task<HotelPostDTO> CreateHotel(HotelPostDTO hotelDto);
    public abstract Task<HotelPostDTO> UpdateHotel(Guid hotelID, HotelPostDTO hotelDto);
}
