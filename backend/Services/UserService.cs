using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class UserService : 
    IDeleteService,
    IGetAllElementsService<UserDTO>,
    IGetElementById<UserPostDTO>,
    ICreateSingleElement<UserPostDTO, UserPostDTO>,
    IUpdateElementByID<UserPostDTO, UserPostDTO>
{
    private SingletonBD _singletonBd;
    
    private UserPostConverter _userPostConverter = new UserPostConverter();
    private UserConverter _userConverter = new UserConverter();

    public UserService()
    {
        _singletonBd = SingletonBD.Instance;
    }
    public async Task<UserPostDTO> GetElementById(Guid userId)
    {
        await Task.Delay(10);
        var user = _singletonBd.GetUserById(userId);
        if (user == null)
            throw new Exception("User not found");
        var contact = _singletonBd.GetContactById(user.ContactID);
        var hotel = _singletonBd.GetHotelByUserId(user.UserID);
        return _userPostConverter.Convert(user,contact, hotel);
    }

    public async Task<List<UserDTO>> GetAllElements()
    {
        await Task.Delay(10);
        
        List<UserDTO> result = _singletonBd.GetAllUsers().Select(x =>
        {
            var contact = _singletonBd.GetContactById(x.ContactID);
            var hotel = _singletonBd.GetHotelByUserId(x.UserID);
            return _userConverter.Convert(x, contact, hotel);
        }).ToList();
        
        return result;
    }

    public async Task<UserPostDTO> CreateSingleElement(UserPostDTO userPostDto)
    {
        await Task.Delay(10);
        if (userPostDto != null)
        {
            var newUser = new User
            {
                Name = userPostDto.Name,
                CINumber = userPostDto.CINumber,
                ContactID = Guid.NewGuid(),
                UserID = Guid.NewGuid(),
            };
            _singletonBd.AddUser(newUser);
            return userPostDto;
        }
        throw new Exception("Contact not Data found");

    }



    public async Task<UserPostDTO> UpdateElementById(Guid userId, UserPostDTO userPostDto)
    {
        await Task.Delay(10);
    
        var existingUser = _singletonBd.GetUserById(userId);
        if (existingUser == null)
            throw new Exception("User not found");

        existingUser.Name = userPostDto.Name;
        existingUser.CINumber = userPostDto.CINumber;

        var contact = _singletonBd.GetContactById(existingUser.ContactID);
        if (contact != null)
        {
            contact.PhoneNumber = userPostDto.PhoneNumber; 
            contact.Email = userPostDto.Email;
            _singletonBd.UpdateContact(contact); 
        }

        _singletonBd.UpdateUser(existingUser);
    
        var relatedHotels = _singletonBd.GetAllHotels().Where(h => h.UserID == userId).ToList();
    
        return _userPostConverter.Convert(existingUser, contact, relatedHotels);
    }

    public async Task<bool> DeleteElementById(Guid elementId)
    {
        await Task.Delay(10);
        return _singletonBd.DeleteUser(elementId);
    }
}
