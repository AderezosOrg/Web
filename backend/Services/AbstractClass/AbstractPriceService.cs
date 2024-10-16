using DTOs.WithoutId;

namespace backend.Services.AbstractClass;

public abstract class AbstractPriceService
{
    public abstract Task<decimal> GetReservationPrice( params ReservationPostDTO[] reservationsPostDtos);
    public abstract Task<decimal> GetReservationPartialPrice( params ReservationPostDTO[] reservationsPostDtos);
}
