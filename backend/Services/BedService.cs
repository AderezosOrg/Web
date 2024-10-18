using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Converters.ToDTO;
using Db;
using DTOs.WithId;
using DTOs.WithoutId;
using Entities;

namespace backend.Services;

using ServicesInterfaces;
using System.Collections.Generic;
using System.Linq;

public class BedService : IBedService
{
    private IDAO<Bed> _bedDao;
    
    private BedPostConverter _bedPostConverter = new BedPostConverter();
    private BedConverter _bedConverter = new BedConverter();

    public BedService(IDAO<Bed> bedDao)
    {
        _bedDao = bedDao;
    }
    public async Task<List<BedInfoDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<BedInfoDTO> result = _bedDao.ReadAll().Select(b =>
        {
            return _bedConverter.Convert(b);
        }).ToList();

        return result;
    }

    public async Task<BedPostDTO> GetElementById(Guid bedID)
    {
        await Task.Delay(10);
        var bed = _bedDao.Read(bedID);
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
            _bedDao.Create(newBed);
            return bedPostDto;
        }
        throw new Exception("Bed not data found");
    }

    public async Task<BedPostDTO> UpdateElementById(Guid bedID, BedPostDTO bedDto)
    {
        await Task.Delay(100);
        var bed = new Bed()
        {
            BedID = bedID,
            Capacity = bedDto.Capacity,
            Size = bedDto.Size,
        };
        _bedDao.Update(bed);
        return _bedPostConverter.Convert(bed);
    }

    public async Task<bool> DeleteElementById(Guid elementId)
    {
        await Task.Delay(100); 
        return _bedDao.Delete(elementId);
    }
}

