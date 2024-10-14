using backend.MyHappyBD;
using backend.Services.AbstractClass;
using Converters.ToDTO;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

public class PriceService : AbstractPriceService
{
    private SingletonBD _singletonBd;
    private ReservationConverter _reservationConverter;

    public PriceService()
    {
        _singletonBd = SingletonBD.Instance;
        _reservationConverter = new ReservationConverter();
    }

    public override async Task<decimal> GetReservationPrice(params ReservationPostDTO[] reservationsPostDtos)
    {
        decimal taxedPrice = 0;
        foreach (ReservationPostDTO reservationsPostDto in reservationsPostDtos)
        {
            var room = _singletonBd.GetRoomById(reservationsPostDto.RoomId);
            var hotelTax = _singletonBd.GetHotelById(room.HotelID).Tax / 100;
            taxedPrice += room.PricePerNight + room.PricePerNight * hotelTax;
        }
        return taxedPrice;
    }
}