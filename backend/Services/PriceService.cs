using backend.MyHappyBD;
using backend.Services.AbstractClass;
using Converters.ToDTO;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

public class PriceService : IPriceService
{
    private SingletonBD _singletonBd;

    public PriceService()
    {
        _singletonBd = SingletonBD.Instance;
    }

    
    public async Task<decimal> GetReservationPrice(params ReservationPostDTO[] reservationsPostDtos)
    {
        decimal taxedPrice = await GetReservationPriceByANight(reservationsPostDtos);
        taxedPrice *= (reservationsPostDtos.First().UseDate - reservationsPostDtos.First().ReservationDate).Days;
        return taxedPrice;
    }

    public async Task<decimal> GetReservationPartialPrice(params ReservationPostDTO[] reservationsPostDtos)
    {
        decimal partialPrice = 0;
        foreach (ReservationPostDTO reservationsPostDto in reservationsPostDtos)
        {
            var room = _singletonBd.GetRoomById(reservationsPostDto.RoomId);
            partialPrice += room.PricePerNight * (reservationsPostDto.UseDate - reservationsPostDto.ReservationDate).Days;
        }
        return partialPrice;
    }

    public async Task<decimal> GetReservationTaxPrice(params ReservationPostDTO[] reservationsPostDtos)
    {
        decimal taxes = 0;
        foreach (ReservationPostDTO reservationsPostDto in reservationsPostDtos)
        {
            var room = _singletonBd.GetRoomById(reservationsPostDto.RoomId);
            var hotelTax = _singletonBd.GetHotelById(room.HotelID).Tax / 100;
            taxes += room.PricePerNight * hotelTax;
        }
        return taxes;
    }

    private async Task<decimal> GetReservationPriceByANight(params ReservationPostDTO[] reservationsPostDtos)
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
