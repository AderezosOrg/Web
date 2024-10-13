using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class BedConverter : IConverter1To2<Bed, BedInformation, BedDTO>
    {
        public BedDTO Convert(Bed bed, BedInformation bedInformation)
        {
            return new BedDTO()
            {
                BedID = bed.BedID,
                Size = bed.Size,
                Capacity = bed.Capacity,
                BedQuantity = bedInformation.Quantity
            };
        }
    }
}