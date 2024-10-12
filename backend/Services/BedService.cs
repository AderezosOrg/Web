using backend.Converters.ToPostDTO;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

using backend.Services.AbstractClass;
using System.Collections.Generic;
using System.Linq;

public class BedService : AbstractBedService
{
    private static List<Bed> _beds = new List<Bed>()
    {
        new Bed()
        {
            BedID = Guid.NewGuid(),
            Capacity = "2",
            Size = "king"
        },
        new Bed()
        {
            BedID = Guid.NewGuid(),
            Capacity = "1",
            Size = "single"
        }
    };

    private List<BedInformation> _bedInformations = new List<BedInformation>()
    {
        new BedInformation()
        {
            BedID = _beds[0].BedID,
            Quantity = 1,
            RoomTemplateID = Guid.NewGuid(),
        },
        new BedInformation()
        {
            BedID = _beds[1].BedID,
            Quantity = 3,
            RoomTemplateID = Guid.NewGuid(),
        }
    };
    
    private BedPostConverter _bedPostConverter = new BedPostConverter();

    public override async Task<List<BedPostDTO>> GetBeds()
    {
        await Task.Delay(10);
        List<BedPostDTO> result = _beds.Select(b =>
        {
            var bedInfo = _bedInformations.FirstOrDefault(bi => bi.BedID == b.BedID);
            return _bedPostConverter.Convert(b, bedInfo);
        }).ToList();

        return result;
    }

    public override async Task<BedPostDTO> GetBedById(Guid bedID)
    {
        await Task.Delay(10);
        var bed = _beds.FirstOrDefault(b => b.BedID == bedID);
        if (bed == null)
            throw new Exception("Bed not found");
        var bedInfo = _bedInformations.FirstOrDefault(bi => bi.BedID == bed.BedID);
        return _bedPostConverter.Convert(bed, bedInfo);
    }

    public override async Task<BedPostDTO> CreateBed(BedPostDTO bedPostDto)
    {
        await Task.Delay(100);
        if (bedPostDto != null)
        {
            var newBed = new Bed
            {
                BedID = Guid.NewGuid(),
                Capacity = bedPostDto.Capacity,
                Size = bedPostDto.Size,
                
            };
            _beds.Add(newBed);
            
            var bedInfo = new BedInformation
            {
                BedID = newBed.BedID,
                Quantity = bedPostDto.BedQuantity,
                RoomTemplateID = Guid.NewGuid()
            };
            _bedInformations.Add(bedInfo);
            if (_beds.Contains(newBed) && _bedInformations.Contains(bedInfo))
                return bedPostDto;
            else
                throw new Exception("Bed not created");
        }
        throw new Exception("Bed not data found");
    }

    public override async Task<BedPostDTO> EditBed(Guid bedID, BedPostDTO bedDto)
    {
        await Task.Delay(100);
        var existingBed = _beds.FirstOrDefault(b => b.BedID == bedID);
        if (existingBed != null)
        {
            existingBed.Capacity = bedDto.Capacity;
            existingBed.Size = bedDto.Size;
            
            var bedInfo = _bedInformations.FirstOrDefault(bi => bi.BedID == bedID);
            if (bedInfo != null)
            {
                bedInfo.Quantity = bedDto.BedQuantity;
            }
            else
            {
                bedInfo = new BedInformation
                {
                    BedID = existingBed.BedID,
                    Quantity = bedDto.BedQuantity,
                    RoomTemplateID = Guid.NewGuid()
                };
                _bedInformations.Add(bedInfo);
            }

            return bedDto;
        }
        throw new Exception("Bed not found");
    }

    public override async Task<bool> DeleteBed(Guid bedID)
    {
        await Task.Delay(100);
        var bed = _beds.FirstOrDefault(b => b.BedID == bedID);
        if (bed != null)
        {
            _beds.Remove(bed);
            if (!_beds.Contains(bed))
                return true;
            else
                throw new Exception("Bed not deleted");
        }
        else
            throw new Exception("Bed not found");
    }
}

