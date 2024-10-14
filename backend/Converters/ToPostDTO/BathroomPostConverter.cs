using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class BathroomPostConverter : IConverter1To1<Bathroom, BathroomPostDTO>
{
    public BathroomPostDTO Convert(Bathroom bathroom)
    {
        return new BathroomPostDTO()
        {
            Shower = bathroom.Shower,
            Toilet = bathroom.Toilet,
            DressingTable = bathroom.DressingTable,
        };
    }
}

