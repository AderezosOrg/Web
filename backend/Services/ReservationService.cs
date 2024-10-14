using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;

namespace backend.Services;

public class ReservationService : AbstractReservationService
{
    private static List<Reservation> _reservations = new List<Reservation>()
    {
        new Reservation()
        {
            Cancelled = false,
            ContactID = Guid.NewGuid(),
            ReservationDate = DateTime.Now,
            RoomID = Guid.NewGuid(),
            UseDate = DateTime.Today,
        },
        new Reservation()
        {
            Cancelled = true,
            ContactID = Guid.NewGuid(),
            ReservationDate = DateTime.Now,
            RoomID = Guid.NewGuid(),
            UseDate = DateTime.Today,
        }
    };

    private List<Contact> _contacts = new List<Contact>()
    {
        new Contact()
        {
            ContactID = _reservations[0].ContactID,
            Email = "test@test.com",
            PhoneNumber = "123456",
        },
        new Contact()
        {
            ContactID = _reservations[1].ContactID,
            Email = "not@test.com",
            PhoneNumber = "657394",
        }
    };

    private List<Room> _rooms = new List<Room>()
    {
        new Room()
        {
            RoomID = _reservations[0].RoomID,
            Code = "A1",
            FloorNumber = 123,
            HotelID = Guid.NewGuid(),
            PricePerNight = 8m,
            RoomTemplateID = Guid.NewGuid(),
        },
        new Room()
        {
            RoomID = _reservations[1].RoomID,
            Code = "A2",
            FloorNumber = 124,
            HotelID = Guid.NewGuid(),
            PricePerNight = 9m,
            RoomTemplateID = Guid.NewGuid(),
        }
    };
    
    private ReservationConverter _reservationPostConverter = new ReservationConverter();


    public override async Task<List<ReservationDTO>> GetReservationsByContactId(Guid contactId)
    {
        await Task.Delay(20);
        var reservations = _reservations.Where(r => r.ContactID == contactId).ToList();
        
        var reservationDTOs = new List<ReservationDTO>();
        foreach (var reservation in reservations)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactID == reservation.ContactID);
            var room = _rooms.FirstOrDefault(r => r.RoomID == reservation.RoomID);
            reservationDTOs.Add(_reservationPostConverter.Convert(reservation, contact, room));
        }
        return reservationDTOs;

    }

    public override async Task<List<ReservationDTO>> GetReservationsByRoomId(Guid roomId)
    {
        await Task.Delay(20);
        var reservations = _reservations.Where(r => r.RoomID == roomId).ToList();
        
        var reservationDTOs = new List<ReservationDTO>();
        foreach (var reservation in reservations)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactID == reservation.ContactID);
            var room = _rooms.FirstOrDefault(r => r.RoomID == reservation.RoomID);
            reservationDTOs.Add(_reservationPostConverter.Convert(reservation, contact, room));
        }
        return reservationDTOs;
    }

    public override async Task<List<ReservationDTO>> GetReservations()
    {
        await Task.Delay(10);
        List<ReservationDTO> result = _reservations.Select(r =>
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactID == r.ContactID);
            var room = _rooms.FirstOrDefault(x => x.RoomID == r.RoomID);
            return _reservationPostConverter.Convert(r, contact, room);
        }).ToList();
        
        return result;
    }

    public override async Task<ReservationDTO> CreateReservation(ReservationDTO reservationDto)
    {
        await Task.Delay(100);
        if (reservationDto != null)
        {
            var newReservation = new Reservation
            {
                ContactID = Guid.NewGuid(),
                Cancelled = reservationDto.Cancelled,
                ReservationDate = reservationDto.ReservationDate,
                RoomID = Guid.NewGuid(),
                UseDate = reservationDto.UseDate,
            };
            _reservations.Add(newReservation);

            var newContact = new Contact
            {
                ContactID = newReservation.ContactID,
                Email = reservationDto.UserEmail,
                PhoneNumber = reservationDto.UserPhoneNumber,
            };
            _contacts.Add(newContact);

            var newRoom = new Room
            {
                RoomID = reservationDto.RoomID,
                Code = reservationDto.RoomCode,
                FloorNumber = reservationDto.RoomFloorNumber,
                HotelID = Guid.NewGuid(),
                PricePerNight = reservationDto.PricePerNight
            };
            _rooms.Add(newRoom);
            if(_reservations.Contains(newReservation) && _contacts.Contains(newContact) && _rooms.Contains(newRoom))
                return reservationDto;
            else 
                throw new Exception("Reservation not created");
        }

        throw new Exception("Reservation data not found");
    }

    public override async Task<ReservationDTO> CancelReservation(Guid contactId)
    {
        await Task.Delay(10);
        var reservation = _reservations.FirstOrDefault(x => x.ContactID == contactId);
        var contact = _contacts.FirstOrDefault(x => x.ContactID == reservation.ContactID);
        var room = _rooms.FirstOrDefault(x => x.RoomID == reservation.RoomID);
        if (reservation != null)
        {
            reservation.Cancelled = false;
            if (reservation.Cancelled == false)
                return _reservationPostConverter.Convert(reservation, contact, room);
            else
                throw new Exception("Reservation not cancelled");

        }
        throw new Exception("Reservation not found");
    }
}
