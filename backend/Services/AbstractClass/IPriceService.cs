using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public interface IPriceService
{
    public abstract Task<decimal> GetReservationPrice( params ReservationPostDTO[] reservationsPostDtos);
    public abstract Task<decimal> GetReservationPartialPrice( params ReservationPostDTO[] reservationsPostDtos);
    
    public abstract Task<decimal> GetReservationTaxPrice( params ReservationPostDTO[] reservationsPostDtos);
}
