using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class BathroomPostConverter : IConverter1To2<Bathroom, RoomBathInformation, BathroomPostDTO>
{
    public BathroomPostDTO Convert(Bathroom bathroom, RoomBathInformation roomBathInformation)
    {
        return new BathroomPostDTO()
        {
            Shower = bathroom.Shower,
            Toilet = bathroom.Toilet,
            DressingTable = bathroom.DressingTable,
            BathroomQuantity = roomBathInformation.Quantity
        };
    }
}

