using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class BedConverter : IConverter1To2<Bed, BedInformation, BedDTO>, IConverter1To2<BedPostDTO, Guid, BedDTO>, IConverter1To1<Bed, BedInfoDTO>
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


        public BedDTO Convert(BedPostDTO bedPostDto, Guid id)
        {
            return new BedDTO()
            {
                BedID = id,
                Capacity = bedPostDto.Capacity,
                Size = bedPostDto.Size
            };
        }

        public BedInfoDTO Convert(Bed input)
        {
            return new BedInfoDTO()
            {
                BedID = input.BedID,
                Capacity = input.Capacity,
                Size = input.Size,
            };
        }
    }
}
