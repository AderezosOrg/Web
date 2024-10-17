using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;
using Db;

namespace backend.Services;

public class UserService : 
    IDeleteService,
    IGetAllElementsService<UserDTO>,
    IGetElementById<UserPostDTO>,
    ICreateSingleElement<UserPostDTO, UserPostDTO>,
    IUpdateElementByID<UpdateUserDTO, UserPostDTO>
{
    private UserDAO _userDAO;
    private ContactDAO _contactDAO;
    private HotelDAO _hotelDao;
    private UserPostConverter _userPostConverter = new UserPostConverter();
    private UserConverter _userConverter = new UserConverter();

    public UserService(UserDAO userDAO, ContactDAO contactDAO, HotelDAO hotelDAO)
    {
        _userDAO = userDAO;
        _contactDAO = contactDAO;
        _hotelDao = hotelDAO;
    }
    public async Task<UserPostDTO> GetElementById(Guid userId)
    {
        await Task.Delay(10);
        var user = _userDAO.Read(userId);
        if (user == null)
            throw new Exception("User not found");
        var contact = _contactDAO.Read(user.ContactID);
        var hotels = _hotelDao.ReadAll().Where(hotel1 => hotel1.UserID == user.UserID).ToList();
        return _userPostConverter.Convert(user,contact, hotels);
    }

    public async Task<List<UserDTO>> GetAllElements()
    {
        await Task.Delay(10);
        
        List<UserDTO> result = _userDAO.ReadAll().Select(x =>
        {
            var contact = _contactDAO.Read(x.ContactID);
            var hotels = _hotelDao.ReadAll().Where(hotel1 => hotel1.UserID == x.UserID).ToList();
            return _userConverter.Convert(x, contact, hotels);
        }).ToList();
        
        return result;
    }

    public async Task<UserPostDTO> CreateSingleElement(UserPostDTO userPostDto)
    {
        await Task.Delay(10);
        if (userPostDto != null)
        {
            var contactId = Guid.NewGuid();
            var contact = new Contact()
            {
                ContactID = contactId,
                Email = userPostDto.Email,
                PhoneNumber = userPostDto.PhoneNumber,
            };
            var newUser = new User
            {
                Name = userPostDto.Name,
                CINumber = userPostDto.CINumber,
                ContactID = contactId,
                UserID = Guid.NewGuid(),
            };
            _userDAO.Create(newUser);
            return userPostDto;
        }
        throw new Exception("Contact not Data found");

    }



    public async Task<UserPostDTO> UpdateElementById(Guid userId, UpdateUserDTO userPostDto)
    {
        await Task.Delay(10);
        var oldUser = _userDAO.Read(userId);
        var newUser = new User()
        {
            UserID = userId,
            CINumber = userPostDto.CINumber,
            Name = userPostDto.Name,
            ContactID = oldUser.ContactID,
        };
        _userDAO.Update(newUser);
        var hotels = _hotelDao.ReadAll().Where(hotel => hotel.UserID == userId).ToList();
        return _userPostConverter.Convert(newUser, _contactDAO.Read(oldUser.ContactID), hotels);
    }

    public async Task<bool> DeleteElementById(Guid elementId)
    {
        await Task.Delay(10);
        return _userDAO.Delete(elementId);
    }
}
