using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO;
public class BathroomConverter : IConverter1To2<Bathroom, RoomBathInformation, BathroomDTO>
{
    public BathroomDTO Convert(Bathroom bathroom, RoomBathInformation roomBathInformation)
    {
        return new BathroomDTO
        {
            BathRoomID = bathroom.BathRoomID,
            Shower = bathroom.Shower,
            Toilet = bathroom.Toilet,
            DressingTable = bathroom.DressingTable,
            BathroomQuantity = roomBathInformation.Quantity
        };
    }
}
