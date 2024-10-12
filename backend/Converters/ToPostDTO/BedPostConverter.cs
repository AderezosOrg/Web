using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class BedPostConverter : IConverter1To2<Bed, BedInformation, BedPostDTO>
{
    public BedPostDTO Convert(Bed bed, BedInformation bedInformation)
    {
        return new BedPostDTO()
        {
            Size = bed.Size,
            Capacity = bed.Capacity,
            BedQuantity = bedInformation.Quantity
        };
    }
}