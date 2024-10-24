using DTOs.WithoutId;

namespace backend.Services.ServicesInterfaces;

public interface IPriceService
{
    public Task<decimal> GetReservationPrice(PriceRequestsDTO reservations);
    public Task<decimal> GetReservationPartialPrice(PriceRequestsDTO reservations);
    
    public Task<decimal> GetReservationTaxPrice(PriceRequestsDTO reservations);
    public Task<decimal> GetReservationPriceByANight(PriceRequestsDTO reservations);

}
