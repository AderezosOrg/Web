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

    public Bathroom UpdateBed(Bathroom entity)
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
}