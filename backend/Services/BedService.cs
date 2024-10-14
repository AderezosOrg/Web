using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

using backend.Services.AbstractClass;
using System.Collections.Generic;
using System.Linq;

public class BedService : AbstractBedService
{
    private SingletonBD _singletonBD;
    private List<Bed> _beds = new List<Bed>();
    
    private List<BedInformation> _bedInformations = new List<BedInformation>();
    
    private BedPostConverter _bedPostConverter = new BedPostConverter();
    private BedConverter _bedConverter = new BedConverter();

    public BedService()
    {
        _singletonBD = SingletonBD.Instance;
        _beds = _singletonBD.GetAllBeds();
        _bedInformations = _singletonBD.GetAllBedInformation();
    }
    public override async Task<List<BedInfoDTO>> GetBeds()
    {
        await Task.Delay(10);
        _beds = _singletonBD.GetAllBeds();
        List<BedInfoDTO> result = _beds.Select(b =>
        {
            return _bedConverter.Convert(b);
        }).ToList();

        return result;
    }

    public override async Task<BedPostDTO> GetBedById(Guid bedID)
    {
        await Task.Delay(10);
        var bed = _singletonBD.GetBedById(bedID);
        if (bed == null)
            throw new Exception("Bed not found");
        return _bedPostConverter.Convert(bed);
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
            _singletonBD.AddBed(newBed);
            
            if (_singletonBD.GetAllBeds().Contains(newBed))
                return bedPostDto;
            else
                throw new Exception("Bed not created");
        }
        throw new Exception("Bed not data found");
    }
    
    public override async Task<BedPostDTO> CreateOnlyBed(BedPostDTO bedPostDto)
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
            _singletonBD.AddBed(newBed);
            
            if (_singletonBD.GetAllBeds().Contains(newBed))
                return bedPostDto;
            else
                throw new Exception("Bed not created");
        }
        throw new Exception("Bed not data found");
    }

    public override async Task<BedPostDTO> EditBed(Guid bedID, BedPostDTO bedDto)
    {
        await Task.Delay(100);
        return _bedPostConverter.Convert(_singletonBD.UpdateBed(new Bed()
        {
            BedID = bedID,
            Capacity = bedDto.Capacity,
            Size = bedDto.Size,
        }));
    }

    public override async Task<bool> DeleteBed(Guid bedID)
    {
        await Task.Delay(100); 
        return _singletonBD.DeleteBed(bedID);
    }
}

