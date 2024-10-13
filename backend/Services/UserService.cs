using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;

namespace backend.Services;

public class UserService : AbstractUserService
{
    private static List<User> _users = new List<User>()
    {
        new User()
        {
            UserID = Guid.NewGuid(),
            Name = "user1",
            CINumber = "6879821",
        },
        new User()
        {
            UserID = Guid.NewGuid(),
            Name = "user2",
            CINumber = "12345678",
        }
    };

    private List<Contact> _contacts = new List<Contact>()
    {
        new Contact()
        {
            ContactID = Guid.NewGuid(),
            Email = "user1@gmail.com",
            PhoneNumber = "12345678",
        },
        new Contact()
        {
            ContactID = Guid.NewGuid(),
            Email = "user2@gmail.com",
            PhoneNumber = "122345678",
        }
    };

    private List<Hotel> _hotels = new List<Hotel>()
    {
        new Hotel()
        {
            HotelID = Guid.NewGuid(),
            AllowsPets = true,
            Address = "address",
            BathRoomID = Guid.NewGuid(),
            ContactID = Guid.NewGuid(),
            Name = "hotel1",
            Stars = 1,
            UserID = Guid.NewGuid(),
        },
        new Hotel()
        {
            HotelID = Guid.NewGuid(),
            AllowsPets = false,
            Address = "address2",
            BathRoomID = Guid.NewGuid(),
            ContactID = Guid.NewGuid(),
            Name = "hotel2",
            Stars = 5,
            UserID = Guid.NewGuid(),
        }
    };
    
    private UserPostConverter _userPostConverter = new UserPostConverter();
    private UserConverter _userConverter = new UserConverter();
    public override async Task<UserPostDTO> GetUserById(Guid userId)
    {
        await Task.Delay(10);
        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user == null)
            throw new Exception("User not found");
        var contact = _contacts.FirstOrDefault(c => c.ContactID == user.ContactID);
        var hotel = _hotels.Where(h => h.UserID == user.UserID).ToList();
        return _userPostConverter.Convert(user,contact, hotel);
    }

    public override async Task<List<UserDTO>> GetUsers()
    {
        await Task.Delay(10);
        List<UserDTO> result = _users.Select(x =>
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactID == x.ContactID);
            var hotel = _hotels.Where(h => h.UserID == x.UserID).ToList();
            return _userConverter.Convert(x, contact, hotel);
        }).ToList();
        
        return result;
    }

    public override async Task<UserPostDTO> CreateUser(UserPostDTO userPostDto)
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
            _users.Add(newUser);

            var newContact = new Contact
            {
                ContactID = Guid.NewGuid(),
                Email = userPostDto.Email,
                PhoneNumber = userPostDto.PhoneNumber,
            };
            _contacts.Add(newContact);
            
            var relatedHotels = _hotels.Where(h => h.ContactID == newContact.ContactID).ToList();
            if (_users.Contains(newUser) && _contacts.Contains(newContact) && relatedHotels.Any())
                return userPostDto;
            else
                throw new Exception("Contact not created");
        }
        throw new Exception("Contact not Data found");

    }

    public override async Task<bool> DeleteUserById(Guid userId)
    {
        await Task.Delay(10);

        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user != null)
        {
            _users.Remove(user);

            var contact = _contacts.FirstOrDefault(c => c.ContactID == user.ContactID);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }

            var relatedHotels = _hotels.Where(h => h.ContactID == user.ContactID).ToList();
            foreach (var hotel in relatedHotels)
            {
                _hotels.Remove(hotel);
            }

            return true; 
        }

        throw new Exception("User not found");
    }

    public override async Task<UserPostDTO> EditUserById(Guid userId, UserPostDTO userPostDto)
    {
        await Task.Delay(10);
        var existingUser = _users.FirstOrDefault(u => u.UserID == userId);
        if (existingUser != null)
        {
            existingUser.Name = userPostDto.Name;
            existingUser.CINumber = userPostDto.CINumber;

            var existingContact = _contacts.FirstOrDefault(c => c.ContactID == existingUser.ContactID);
            if (existingContact != null)
            {
                existingContact.Email = userPostDto.Email;
                existingContact.PhoneNumber = userPostDto.PhoneNumber;
            }

            var relatedHotels = _hotels.Where(h => h.ContactID == existingUser.ContactID).ToList();

            var userPostConverter = new UserPostConverter();
            return userPostConverter.Convert(existingUser, existingContact, relatedHotels);
        }

        throw new Exception("User not found");
    }
}
