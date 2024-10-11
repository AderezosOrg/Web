using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    [HttpGet("{hotelId}")]
    public ActionResult<HotelDTO> GetHotelById(Guid hotelId)
    {
        return Ok(new HotelDTO
        {
            Address = "123'3'2",
            AllowsPets = false,
            DressingTable = true,
            HotelEmail = "test@testeado.com",
            HotelID = Guid.NewGuid(),
            HotelPhoneNumber = "123123123",
            Name = "My Hotel",
            Shower = false,
            Stars = 3,
            Toilet = false,
            UserEmail = "user@testeado.com",
            UserPhoneNumber = "+23456789",
            UserCINumber = "1272012",
            UserName = "Paco"
            
        });
    }
    
    [HttpGet]
    public ActionResult<List<HotelDTO>> GetHotels()
    {
        return Ok(new List<HotelDTO>
        {
            new HotelDTO
            {
                Address = "123'3'2",
                AllowsPets = false,
                DressingTable = true,
                HotelEmail = "test@testeado.com",
                HotelID = Guid.NewGuid(),
                HotelPhoneNumber = "123123123",
                Name = "My Hotel",
                Shower = false,
                Stars = 3,
                Toilet = false,
                UserEmail = "user@testeado.com",
                UserPhoneNumber = "+23456789",
                UserCINumber = "1272012",
                UserName = "Paco"
            
            }
        });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateHotel([FromBody] HotelDTO hotelDto)
    {
        return Ok(true);
    }
    
    [HttpPut("{hotelId}")]
    public ActionResult<bool> UpdateHotel(Guid hotelId, HotelDTO hotelDto)
    {
        return Ok(true);
    }
    
}
