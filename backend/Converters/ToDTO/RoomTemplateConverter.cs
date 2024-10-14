using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO;

public class RoomTemplateConverter: IConverter1To5<RoomTemplate, List<RoomBathInformation>, List<Bathroom>, List<BedInformation>, List<Bed>, RoomTemplateDTO>, 
    IConverter1To3<RoomTemplate, List<BathroomDTO>, List<BedDTO>, RoomTemplateDTO>
{
    public RoomTemplateDTO Convert(RoomTemplate input1, List<RoomBathInformation> input2, List<Bathroom> input3, List<BedInformation> input4, List<Bed> input5)
    {
        List<BathroomDTO> bathrooms = new List<BathroomDTO>();
        List<BedDTO> beds = new List<BedDTO>();
        foreach (RoomBathInformation roomBathInformation in input2)
        {
            if (roomBathInformation.RoomTemplateID == input1.RoomTemplateID)
            {
                BathroomDTO bathroom = new BathroomConverter().Convert(
                    input3.FirstOrDefault(b => b.BathRoomID == roomBathInformation.BathRoomID), roomBathInformation);
                bathrooms.Add(bathroom);    
            }
            
        }
        
        foreach (BedInformation bedInformation in input4)
        {
            if (bedInformation.RoomTemplateID == input1.RoomTemplateID)
            {
                BedDTO bedDto = new BedConverter().Convert(
                    input5.FirstOrDefault(b => b.BedID == bedInformation.BedID), bedInformation);
                beds.Add(bedDto);
            }
        }
        return Convert(input1, bathrooms, beds);
    }
    
    public RoomTemplateDTO Convert(RoomTemplate input1, List<BathroomDTO> input2, List<BedDTO> input3)
    {
        return new RoomTemplateDTO()
        {
            RoomTemplateID = input1.RoomTemplateID,
            Bathrooms = input2,
            Beds = input3,
            Side = input1.Side,
            Windows = input1.Windows,
        };
    }
}