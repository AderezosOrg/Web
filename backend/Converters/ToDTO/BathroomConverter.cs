using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace Converters.ToDTO;
public class BathroomConverter : IConverter1To2<Bathroom, RoomBathInformation, BathroomDTO>, IConverter1To2<BathroomPostDTO, Guid, BathroomDTO>, IConverter1To1<Bathroom, BathroomInfoDTO>
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
            DressingTable = postDto.DressingTable,
            Shower = postDto.Shower,
            Toilet = postDto.Toilet
        };
    }

    public BathroomInfoDTO Convert(Bathroom input)
    {
        return new BathroomInfoDTO()
        {
            BathRoomID = input.BathRoomID,
            DressingTable = input.DressingTable,
            Shower = input.Shower,
            Toilet = input.Toilet
        };
    }
}

