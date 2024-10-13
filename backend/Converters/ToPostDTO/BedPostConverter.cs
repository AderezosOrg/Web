using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class BedPostConverter : IConverter1To1<Bed, BedPostDTO>
{
    public BedPostDTO Convert(Bed bed)
    {
        return new BedPostDTO()
        {
            Size = bed.Size,
            Capacity = bed.Capacity,
        };
    }
}