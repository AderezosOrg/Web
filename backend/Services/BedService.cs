using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Converters.ToDTO;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

using ServicesInterfaces;
using System.Collections.Generic;
using System.Linq;

public class BedService :
    IDeleteService,
    IGetAllElementsService<BedInfoDTO>,
    IGetElementById<BedPostDTO>,
    ICreateSingleElement<BedPostDTO, BedPostDTO>,
    IUpdateElementByID<BedPostDTO, BedPostDTO>
{
    private SingletonBD _singletonBD;
    
    
    private BedPostConverter _bedPostConverter = new BedPostConverter();
    private BedConverter _bedConverter = new BedConverter();

    public BedService()
    {
        _singletonBD = SingletonBD.Instance;
    }
    public async Task<List<BedInfoDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<BedInfoDTO> result = _singletonBD.GetAllBeds().Select(b =>
        {
            return _bedConverter.Convert(b);
        }).ToList();

        return result;
    }

    public async Task<BedPostDTO> GetElementById(Guid bedID)
    {
        await Task.Delay(10);
        var bed = _singletonBD.GetBedById(bedID);
        if (bed == null)
            throw new Exception("Bed not found");
        return _bedPostConverter.Convert(bed);
    }

    public async Task<BedPostDTO> CreateSingleElement(BedPostDTO bedPostDto)
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
            return bedPostDto;
        }
        throw new Exception("Bed not data found");
    }
    
    public async Task<BedPostDTO> CreateOnlyBed(BedPostDTO bedPostDto)
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

    public async Task<BedPostDTO> UpdateElementById(Guid bedID, BedPostDTO bedDto)
    {
        await Task.Delay(100);
        return _bedPostConverter.Convert(_singletonBD.UpdateBed(new Bed()
        {
            BedID = bedID,
            Capacity = bedDto.Capacity,
            Size = bedDto.Size,
        }));
    }

    public async Task<bool> DeleteElementById(Guid elementId)
    {
        await Task.Delay(100); 
        return _singletonBD.DeleteBed(elementId);
    }
}

