namespace backend.Services;

using DTOs;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

public class BedService : IBedService
{
    private List<BedDTO> _beds = new List<BedDTO>
    {
        new BedDTO
        {
            BedID = Guid.NewGuid(),
            BedQuantity = 2,
            Capacity = "2",
            Size = "king"
        },
        new BedDTO
        {
            BedID = Guid.NewGuid(),
            BedQuantity = 1,
            Capacity = "1",
            Size = "single"
        }
    };

    public List<BedDTO> GetBeds()
    {
        return _beds;
    }

    public BedDTO GetBedById(Guid bedID)
    {
        return _beds.FirstOrDefault(b => b.BedID == bedID);
    }

    public bool CreateBed(BedDTO bedDto)
    {
        _beds.Add(bedDto);
        return true;
    }

    public BedDTO EditBed(Guid bedID, BedDTO bedDto)
    {
        var existingBed = _beds.FirstOrDefault(b => b.BedID == bedID);
        if (existingBed != null)
        {
            existingBed.BedQuantity = bedDto.BedQuantity;
            existingBed.Capacity = bedDto.Capacity;
            existingBed.Size = bedDto.Size;
        }
        return existingBed;
    }

    public bool DeleteBed(Guid bedID)
    {
        var bed = _beds.FirstOrDefault(b => b.BedID == bedID);
        if (bed != null)
        {
            _beds.Remove(bed);
            return true;
        }
        return false;
    }
}

