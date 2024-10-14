using Entities;

namespace backend.MyHappyBD;

public class SingletonBD
{
    private static SingletonBD _instance = null;
    private static List<Bed> _beds = new List<Bed>();
    private static List<Contact> _contacts = new List<Contact>();
    private static List<Room> _rooms = new List<Room>();
    private static List<Bathroom> _bathrooms = new List<Bathroom>();
    private static List<BedInformation> _bedInformations = new List<BedInformation>();
    private static List<RoomBathInformation> _roomBathInformations = new List<RoomBathInformation>();
    private static List<Hotel> _hotels = new List<Hotel>();
    private static List<Service> _services = new List<Service>();
    private static List<RoomServices> _roomServices = new List<RoomServices>();
    private static List<User> _users = new List<User>();
    private static List<Reservation> _reservations = new List<Reservation>();
    private static List<RoomTemplate> _roomTemplates = new List<RoomTemplate>();

    public static SingletonBD Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SingletonBD();
            }
            return _instance;
        }
    }

    private SingletonBD()
    {
        _beds = JsonManager.ReadJsonAsync<List<Bed>>("bed.json").Result;
        _bathrooms = JsonManager.ReadJsonAsync<List<Bathroom>>("bathroom.json").Result;
        _contacts = JsonManager.ReadJsonAsync<List<Contact>>("contact.json").Result;
        _rooms = JsonManager.ReadJsonAsync<List<Room>>("room.json").Result;
        _hotels = JsonManager.ReadJsonAsync<List<Hotel>>("hotel.json").Result;
        _services = JsonManager.ReadJsonAsync<List<Service>>("service.json").Result;
        _roomServices = JsonManager.ReadJsonAsync<List<RoomServices>>("roomServices.json").Result;
        _users = JsonManager.ReadJsonAsync<List<User>>("user.json").Result;
        _reservations = JsonManager.ReadJsonAsync<List<Reservation>>("reservation.json").Result;
        _bedInformations = JsonManager.ReadJsonAsync<List<BedInformation>>("bedInformation.json").Result;
        _roomBathInformations = JsonManager.ReadJsonAsync<List<RoomBathInformation>>("roomBathInformation.json").Result;
        _roomTemplates = JsonManager.ReadJsonAsync<List<RoomTemplate>>("roomTemplate.json").Result;
    }

    private void ReloadDatabase()
    {
        _beds = JsonManager.ReadJsonAsync<List<Bed>>("bed.json").Result;
        _bathrooms = JsonManager.ReadJsonAsync<List<Bathroom>>("bathroom.json").Result;
        _contacts = JsonManager.ReadJsonAsync<List<Contact>>("contact.json").Result;
        _rooms = JsonManager.ReadJsonAsync<List<Room>>("room.json").Result;
        _hotels = JsonManager.ReadJsonAsync<List<Hotel>>("hotel.json").Result;
        _services = JsonManager.ReadJsonAsync<List<Service>>("service.json").Result;
        _roomServices = JsonManager.ReadJsonAsync<List<RoomServices>>("roomServices.json").Result;
        _users = JsonManager.ReadJsonAsync<List<User>>("user.json").Result;
        _reservations = JsonManager.ReadJsonAsync<List<Reservation>>("reservation.json").Result;
        _bedInformations = JsonManager.ReadJsonAsync<List<BedInformation>>("bedInformation.json").Result;
        _roomBathInformations = JsonManager.ReadJsonAsync<List<RoomBathInformation>>("roomBathInformation.json").Result;
        _roomTemplates = JsonManager.ReadJsonAsync<List<RoomTemplate>>("roomTemplate.json").Result;
    }
    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
    }

    //Beds
    public List<Bed> GetAllBeds()
    {
        ReloadDatabase();
        return _beds;
    }

    

    public Bed GetBedById(Guid id)
    {
        ReloadDatabase();
        return _beds.FirstOrDefault(b => b.BedID == id);
    }

    public Bed AddBed(Bed entity)
    {
        ReloadDatabase();
        _beds.Add(entity);
        JsonManager.WriteJsonAsync<List<Bed>>("bed.json", _beds);
        ReloadDatabase();
        return _beds.FirstOrDefault(b => b.BedID == entity.BedID);
    }

    public Bed UpdateBed(Bed entity)
    {
        ReloadDatabase();
        _beds.Remove(_beds.FirstOrDefault(b => b.BedID == entity.BedID));
        _beds.Add(entity);
        JsonManager.WriteJsonAsync<List<Bed>>("bed.json", _beds);
        ReloadDatabase();
        return _beds.FirstOrDefault(b => b.BedID == entity.BedID);
    }

    public bool DeleteBed(Guid id)
    {
        ReloadDatabase();
        if (_beds.Remove(_beds.FirstOrDefault(b => b.BedID == id)))
        {
            JsonManager.WriteJsonAsync<List<Bed>>("bed.json", _beds);
            return true;
        }
        return false;
    }
    
    //Bathrooms
    public List<Bathroom> GetAllBathRooms()
    {
        ReloadDatabase();
        return _bathrooms;
    }

    

    public Bathroom GetBathRoomById(Guid id)
    {
        ReloadDatabase();
        return _bathrooms.FirstOrDefault(b => b.BathRoomID == id);
    }

    public Bathroom AddBathroom(Bathroom entity)
    {
        ReloadDatabase();
        _bathrooms.Add(entity);
        JsonManager.WriteJsonAsync<List<Bathroom>>("bathroom.json", _bathrooms);
        ReloadDatabase();
        return _bathrooms.FirstOrDefault(b => b.BathRoomID == entity.BathRoomID);
    }

    public Bathroom UpdateBathroom(Bathroom entity)
    {
        ReloadDatabase();
        _bathrooms.Remove(_bathrooms.FirstOrDefault(b => b.BathRoomID == entity.BathRoomID));
        _bathrooms.Add(entity);
        JsonManager.WriteJsonAsync<List<Bathroom>>("bathroom.json", _bathrooms);
        ReloadDatabase();
        return _bathrooms.FirstOrDefault(b => b.BathRoomID == entity.BathRoomID);
    }

    public bool DeleteBathroom(Guid id)
    {
        ReloadDatabase();
        if (_bathrooms.Remove(_bathrooms.FirstOrDefault(b => b.BathRoomID == id)))
        {
            JsonManager.WriteJsonAsync<List<Bathroom>>("bathroom.json", _bathrooms);
            return true;
        }
        return false;
    }
    
    // Contacts
    public List<Contact> GetAllContacts()
    {
        ReloadDatabase();
        return _contacts;
    }

    public Contact GetContactById(Guid id)
    {
        ReloadDatabase();
        return _contacts.FirstOrDefault(c => c.ContactID == id);
    }

    public Contact AddContact(Contact entity)
    {
        ReloadDatabase();
        _contacts.Add(entity);
        JsonManager.WriteJsonAsync<List<Contact>>("contact.json", _contacts);
        ReloadDatabase();
        return _contacts.FirstOrDefault(c => c.ContactID == entity.ContactID);
    }

    public Contact UpdateContact(Contact entity)
    {
        ReloadDatabase();
        _contacts.Remove(_contacts.FirstOrDefault(c => c.ContactID == entity.ContactID));
        _contacts.Add(entity);
        JsonManager.WriteJsonAsync<List<Contact>>("contact.json", _contacts);
        ReloadDatabase();
        return _contacts.FirstOrDefault(c => c.ContactID == entity.ContactID);
    }

    public bool DeleteContact(Guid id)
    {
        ReloadDatabase();
        if (_contacts.Remove(_contacts.FirstOrDefault(c => c.ContactID == id)))
        {
            JsonManager.WriteJsonAsync<List<Contact>>("contact.json", _contacts);
            return true;
        }
        return false;
    }
    
    // Users
    public List<User> GetAllUsers()
    {
        ReloadDatabase();
        return _users;
    }

    public User GetUserById(Guid id)
    {
        ReloadDatabase();
        return _users.FirstOrDefault(u => u.UserID == id);
    }

    public User AddUser(User entity)
    {
        ReloadDatabase();
        _users.Add(entity);
        JsonManager.WriteJsonAsync<List<User>>("user.json", _users);
        ReloadDatabase();
        return _users.FirstOrDefault(u => u.UserID == entity.UserID);
    }

    public User UpdateUser(User entity)
    {
        ReloadDatabase();
        _users.Remove(_users.FirstOrDefault(u => u.UserID == entity.UserID));
        _users.Add(entity);
        JsonManager.WriteJsonAsync<List<User>>("user.json", _users);
        ReloadDatabase();
        return _users.FirstOrDefault(u => u.UserID == entity.UserID);
    }

    public bool DeleteUser(Guid id)
    {
        ReloadDatabase();
        if (_users.Remove(_users.FirstOrDefault(u => u.UserID == id)))
        {
            JsonManager.WriteJsonAsync<List<User>>("user.json", _users);
            return true;
        }
        return false;
    }
    
    // Hotels
    public List<Hotel> GetAllHotels()
    {
        ReloadDatabase();
        return _hotels;
    }

    public Hotel GetHotelById(Guid id)
    {
        ReloadDatabase();
        return _hotels.FirstOrDefault(h => h.HotelID == id);
    }

    public Hotel AddHotel(Hotel entity)
    {
        ReloadDatabase();
        _hotels.Add(entity);
        JsonManager.WriteJsonAsync<List<Hotel>>("hotel.json", _hotels);
        ReloadDatabase();
        return _hotels.FirstOrDefault(h => h.HotelID == entity.HotelID);
    }

    public Hotel UpdateHotel(Hotel entity)
    {
        ReloadDatabase();
        _hotels.Remove(_hotels.FirstOrDefault(h => h.HotelID == entity.HotelID));
        _hotels.Add(entity);
        JsonManager.WriteJsonAsync<List<Hotel>>("hotel.json", _hotels);
        ReloadDatabase();
        return _hotels.FirstOrDefault(h => h.HotelID == entity.HotelID);
    }

    public bool DeleteHotel(Guid id)
    {
        ReloadDatabase();
        if (_hotels.Remove(_hotels.FirstOrDefault(h => h.HotelID == id)))
        {
            JsonManager.WriteJsonAsync<List<Hotel>>("hotel.json", _hotels);
            return true;
        }
        return false;
    }
    
    // Rooms
    public List<Room> GetAllRooms()
    {
        ReloadDatabase();
        return _rooms;
    }

    public Room GetRoomById(Guid id)
    {
        ReloadDatabase();
        return _rooms.FirstOrDefault(r => r.RoomID == id);
    }

    public Room AddRoom(Room entity)
    {
        ReloadDatabase();
        _rooms.Add(entity);
        JsonManager.WriteJsonAsync<List<Room>>("room.json", _rooms);
        ReloadDatabase();
        return _rooms.FirstOrDefault(r => r.RoomID == entity.RoomID);
    }

    public Room UpdateRoom(Room entity)
    {
        ReloadDatabase();
        _rooms.Remove(_rooms.FirstOrDefault(r => r.RoomID == entity.RoomID));
        _rooms.Add(entity);
        JsonManager.WriteJsonAsync<List<Room>>("room.json", _rooms);
        ReloadDatabase();
        return _rooms.FirstOrDefault(r => r.RoomID == entity.RoomID);
    }

    public bool DeleteRoom(Guid id)
    {
        ReloadDatabase();
        if (_rooms.Remove(_rooms.FirstOrDefault(r => r.RoomID == id)))
        {
            JsonManager.WriteJsonAsync<List<Room>>("room.json", _rooms);
            return true;
        }
        return false;
    }

    //BathRoomInformation
    public List<RoomBathInformation> GetAllBathroomInformation()
    {
        ReloadDatabase();
        return _roomBathInformations;
    }

    

    public List<RoomBathInformation> GetBathRoomInformationByBathroomId(Guid id)
    {
        ReloadDatabase();
        return _roomBathInformations.Where(b => b.BathRoomID == id).ToList();
    }
    
    public List<RoomBathInformation> GetBathRoomInformationByRoomTemplateId(Guid id)
    {
        ReloadDatabase();
        return _roomBathInformations.Where(b => b.BathRoomID == id).ToList();
    }

    public RoomBathInformation AddBathroomInformation(RoomBathInformation entity)
    {
        ReloadDatabase();
        _roomBathInformations.Add(entity);
        JsonManager.WriteJsonAsync<List<RoomBathInformation>>("roomBathInformation.json", _roomBathInformations);
        ReloadDatabase();
        return _roomBathInformations.FirstOrDefault(b => b.BathRoomID == entity.BathRoomID && b.RoomTemplateID == entity.RoomTemplateID);
    }

    public RoomBathInformation UpdateBed(RoomBathInformation entity)
    {
        ReloadDatabase();
        _roomBathInformations.Remove(_roomBathInformations.FirstOrDefault(b => b.BathRoomID == entity.BathRoomID && b.RoomTemplateID == entity.RoomTemplateID));
        _roomBathInformations.Add(entity);
        JsonManager.WriteJsonAsync<List<RoomBathInformation>>("roomBathInformation.json", _roomBathInformations);
        ReloadDatabase();
        return _roomBathInformations.FirstOrDefault(b => b.BathRoomID == entity.BathRoomID && b.RoomTemplateID == entity.RoomTemplateID);
    }

    public bool DeleteBathroomInformation(Guid id)
    {
        ReloadDatabase();
        if (_roomBathInformations.Remove(_roomBathInformations.FirstOrDefault(b => b.BathRoomID == id && b.RoomTemplateID == id)))
        {
            JsonManager.WriteJsonAsync<List<RoomBathInformation>>("roomBathInformation.json", _roomBathInformations);
            return true;
        }
        return false;
    }

            
    // RoomTemplates
    public List<RoomTemplate> GetAllRoomTemplates()
    {
        ReloadDatabase();
        return _roomTemplates;
    }

    public RoomTemplate GetRoomTemplateById(Guid id)
    {
        ReloadDatabase();
        return _roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == id);
    }

    public RoomTemplate AddRoomTemplate(RoomTemplate entity)
    {
        ReloadDatabase();
        _roomTemplates.Add(entity);
        JsonManager.WriteJsonAsync<List<RoomTemplate>>("roomTemplate.json", _roomTemplates);
        ReloadDatabase();
        return _roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == entity.RoomTemplateID);
    }

    public RoomTemplate UpdateRoomTemplate(RoomTemplate entity)
    {
        ReloadDatabase();
        _roomTemplates.Remove(_roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == entity.RoomTemplateID));
        _roomTemplates.Add(entity);
        JsonManager.WriteJsonAsync<List<RoomTemplate>>("roomTemplate.json", _roomTemplates);
        ReloadDatabase();
        return _roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == entity.RoomTemplateID);
    }

    public bool DeleteRoomTemplate(Guid id)
    {
        ReloadDatabase();
        if (_roomTemplates.Remove(_roomTemplates.FirstOrDefault(rt => rt.RoomTemplateID == id)))
        {
            JsonManager.WriteJsonAsync<List<RoomTemplate>>("roomTemplate.json", _roomTemplates);
            return true;
        }
        return false;
    }

    // Services
    public List<Service> GetAllServices()
    {
        ReloadDatabase();
        return _services;
    }

    public Service GetServiceById(Guid id)
    {
        ReloadDatabase();
        return _services.FirstOrDefault(s => s.ServiceID == id);
    }

    public Service AddService(Service entity)
    {
        ReloadDatabase();
        _services.Add(entity);
        JsonManager.WriteJsonAsync<List<Service>>("service.json", _services);
        ReloadDatabase();
        return _services.FirstOrDefault(s => s.ServiceID == entity.ServiceID);
    }

    public Service UpdateService(Service entity)
    {
        ReloadDatabase();
        _services.Remove(_services.FirstOrDefault(s => s.ServiceID == entity.ServiceID));
        _services.Add(entity);
        JsonManager.WriteJsonAsync<List<Service>>("service.json", _services);
        ReloadDatabase();
        return _services.FirstOrDefault(s => s.ServiceID == entity.ServiceID);
    }

    public bool DeleteService(Guid id)
    {
        ReloadDatabase();
        if (_services.Remove(_services.FirstOrDefault(s => s.ServiceID == id)))
        {
            JsonManager.WriteJsonAsync<List<Service>>("service.json", _services);
            return true;
        }
        return false;
    }
    
    // BedInformation
    public List<BedInformation> GetAllBedInformation()
    {
        ReloadDatabase();
        return _bedInformations;
    }

    public List<BedInformation> GetBedInformationByBedId(Guid id)
    {
        ReloadDatabase();
        return _bedInformations.Where(bi => bi.BedID == id).ToList();
    }

    public List<BedInformation> GetBedInformationByRoomTemplateId(Guid id)
    {
        ReloadDatabase();
        return _bedInformations.Where(bi => bi.RoomTemplateID == id).ToList();
    }

    public BedInformation AddBedInformation(BedInformation entity)
    {
        ReloadDatabase();
        _bedInformations.Add(entity);
        JsonManager.WriteJsonAsync<List<BedInformation>>("bedInformation.json", _bedInformations);
        ReloadDatabase();
        return _bedInformations.FirstOrDefault(bi => bi.BedID == entity.BedID && bi.RoomTemplateID == entity.RoomTemplateID);
    }

    public BedInformation UpdateBedInformation(BedInformation entity)
    {
        ReloadDatabase();
        _bedInformations.Remove(_bedInformations.FirstOrDefault(bi => bi.BedID == entity.BedID && bi.RoomTemplateID == entity.RoomTemplateID));
        _bedInformations.Add(entity);
        JsonManager.WriteJsonAsync<List<BedInformation>>("bedInformation.json", _bedInformations);
        ReloadDatabase();
        return _bedInformations.FirstOrDefault(bi => bi.BedID == entity.BedID && bi.RoomTemplateID == entity.RoomTemplateID);
    }

    public bool DeleteBedInformation(Guid bedId, Guid roomTemplateId)
    {
        ReloadDatabase();
        if (_bedInformations.Remove(_bedInformations.FirstOrDefault(bi => bi.BedID == bedId && bi.RoomTemplateID == roomTemplateId)))
        {
            JsonManager.WriteJsonAsync<List<BedInformation>>("bedInformation.json", _bedInformations);
            return true;
        }
        return false;
    }
    
    // RoomServices
    public List<RoomServices> GetAllRoomServices()
    {
        ReloadDatabase();
        return _roomServices;
    }

    public List<RoomServices> GetRoomServicesByRoomId(Guid roomId)
    {
        ReloadDatabase();
        return _roomServices.Where(rs => rs.RoomID == roomId).ToList();
    }

    public List<RoomServices> GetRoomServicesByServiceId(Guid serviceId)
    {
        ReloadDatabase();
        return _roomServices.Where(rs => rs.ServiceID == serviceId).ToList();
    }

    public RoomServices AddRoomServices(RoomServices entity)
    {
        ReloadDatabase();
        _roomServices.Add(entity);
        JsonManager.WriteJsonAsync<List<RoomServices>>("roomServices.json", _roomServices);
        ReloadDatabase();
        return _roomServices.FirstOrDefault(rs => rs.RoomID == entity.RoomID && rs.ServiceID == entity.ServiceID);
    }

    public RoomServices UpdateRoomServices(RoomServices entity)
    {
        ReloadDatabase();
        _roomServices.Remove(_roomServices.FirstOrDefault(rs => rs.RoomID == entity.RoomID && rs.ServiceID == entity.ServiceID));
        _roomServices.Add(entity);
        JsonManager.WriteJsonAsync<List<RoomServices>>("roomServices.json", _roomServices);
        ReloadDatabase();
        return _roomServices.FirstOrDefault(rs => rs.RoomID == entity.RoomID && rs.ServiceID == entity.ServiceID);
    }

    public bool DeleteRoomServices(Guid roomId, Guid serviceId)
    {
        ReloadDatabase();
        if (_roomServices.Remove(_roomServices.FirstOrDefault(rs => rs.RoomID == roomId && rs.ServiceID == serviceId)))
        {
            JsonManager.WriteJsonAsync<List<RoomServices>>("roomServices.json", _roomServices);
            return true;
        }
        return false;
    }
    
    // Reservations
    public List<Reservation> GetAllReservations()
    {
        ReloadDatabase();
        return _reservations;
    }
    
    
    public List<Reservation> GetReservationByContactId(Guid id)
    {
        ReloadDatabase();
        return _reservations.Where(r => r.ContactID == id).ToList();
    }

    public List<Reservation> GetReservationByRoomId(Guid id)
    {
        ReloadDatabase();
        return _reservations.Where(r => r.RoomID == id).ToList();
    }

    public Reservation AddReservation(Reservation entity)
    {
        ReloadDatabase();
        _reservations.Add(entity);
        JsonManager.WriteJsonAsync<List<Reservation>>("reservation.json", _reservations);
        ReloadDatabase();
        return _reservations.FirstOrDefault(r => r.ContactID == entity.ContactID && r.RoomID == entity.RoomID);
    }

    public Reservation UpdateReservation(Reservation entity)
    {
        ReloadDatabase();
        _reservations.Remove(_reservations.FirstOrDefault(r => r.ContactID == entity.ContactID && r.RoomID == entity.RoomID));
        _reservations.Add(entity);
        JsonManager.WriteJsonAsync<List<Reservation>>("reservation.json", _reservations);
        ReloadDatabase();
        return _reservations.FirstOrDefault(r => r.ContactID == entity.ContactID && r.RoomID == entity.RoomID);
    }

    public bool DeleteReservation(Guid contactId, Guid roomId)
    {
        ReloadDatabase();
        if (_reservations.Remove(_reservations.FirstOrDefault(r => r.ContactID == contactId && r.RoomID == roomId)))
        {
            JsonManager.WriteJsonAsync<List<Reservation>>("reservation.json", _reservations);
            return true;
        }
        return false;
    }
    
}
