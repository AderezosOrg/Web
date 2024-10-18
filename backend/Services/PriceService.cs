using backend.MyHappyBD;
using backend.Services.ServicesInterfaces;
using Db;
using DTOs.WithoutId;

namespace backend.Services;

public class PriceService : IPriceService
{
    private readonly HotelDAO _hotelDao;
    private readonly RoomDAO _roomDao;

    public PriceService(RoomDAO roomDao, HotelDAO hotelDao)
    {
        _roomDao = roomDao;
        _hotelDao = hotelDao;
    }

    
    public async Task<decimal> GetReservationPrice(PriceRequestsDTO reservations)
    {
        decimal taxedPrice = await GetReservationPriceByANight(reservations);
        taxedPrice *= (reservations.Reservations.First().UseDate - reservations.Reservations.First().ReservationDate).Days;
        return taxedPrice;
    }

    public async Task<decimal> GetReservationPartialPrice(PriceRequestsDTO reservations)
    {
        decimal partialPrice = 0;
        foreach (ReservationPostDTO reservationsPostDto in reservations.Reservations)
        {
            var room = _roomDao.Read(reservationsPostDto.RoomId);
            partialPrice += room.PricePerNight * (reservationsPostDto.UseDate - reservationsPostDto.ReservationDate).Days;
        }
        return partialPrice;
    }

    public async Task<decimal> GetReservationTaxPrice(PriceRequestsDTO reservations)
    {
        decimal taxes = 0;
        foreach (ReservationPostDTO reservationsPostDto in reservations.Reservations)
        {
            var room = _roomDao.Read(reservationsPostDto.RoomId);
            var hotelTax = _hotelDao.Read(room.HotelID).Tax / 100;
            taxes += room.PricePerNight * hotelTax  * (reservationsPostDto.UseDate - reservationsPostDto.ReservationDate).Days;
        }
        return taxes;
    }

    private async Task<decimal> GetReservationPriceByANight(PriceRequestsDTO reservations)
    {
        decimal taxedPrice = 0;
        foreach (ReservationPostDTO reservationsPostDto in reservations.Reservations)
        {
            var room = _roomDao.Read(reservationsPostDto.RoomId);
            var hotelTax = _hotelDao.Read(room.HotelID).Tax / 100;
            taxedPrice += room.PricePerNight + room.PricePerNight * hotelTax;
        }
        return taxedPrice;
    }
}
