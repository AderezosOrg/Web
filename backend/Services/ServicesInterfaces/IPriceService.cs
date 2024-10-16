using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public interface IPriceService
{
    public abstract Task<decimal> GetReservationPrice(PriceRequestsDTO reservations);
    public abstract Task<decimal> GetReservationPartialPrice(PriceRequestsDTO reservations);
    
    public abstract Task<decimal> GetReservationTaxPrice(PriceRequestsDTO reservations);
}
