using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace Converters.ToDTO;
public class BathroomConverter : IConverter1To2<Bathroom, RoomBathInformation, BathroomDTO>, IConverter1To2<BathroomPostDTO, Guid, BathroomDTO>
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
    
    public BathroomDTO Convert(BathroomPostDTO postDto, Guid id)
    {
        return new BathroomDTO()
        {
            BathRoomID = id,
            BathroomQuantity = postDto.BathroomQuantity,
            DressingTable = postDto.DressingTable,
            Shower = postDto.Shower,
            Toilet = postDto.Toilet
        };
    }
}

