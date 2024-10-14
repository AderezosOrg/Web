using backend.Services.AbstractClass;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.Converters.ToPostDTO;
using backend.MyHappyBD;

namespace backend.Services;

public class ReservationService : AbstractReservationService
{
    private SingletonBD _singletonBd;
    
    private ReservationConverter _reservationPostConverter = new ReservationConverter();


    public ReservationService()
    {
        _singletonBd = SingletonBD.Instance;
    }

    public override async Task<List<ReservationDTO>> GetReservationsByContactId(Guid contactId)
    {
        await Task.Delay(20);
        var reservations = _singletonBd.GetReservationByContactId(contactId);
        
        var reservationDTOs = new List<ReservationDTO>();
        foreach (var reservation in reservations)
        {
            var contact = _singletonBd.GetContactById(reservation.ContactID);
            var room = _singletonBd.GetRoomById(reservation.RoomID);
            reservationDTOs.Add(_reservationPostConverter.Convert(reservation, contact, room));
        }
        return reservationDTOs;

    }

    public override async Task<List<ReservationDTO>> GetReservationsByRoomId(Guid roomId)
    {
        await Task.Delay(20);
        var reservations = _singletonBd.GetReservationByRoomId(roomId);
        
        var reservationDTOs = new List<ReservationDTO>();
        foreach (var reservation in reservations)
        {
            var contact = _singletonBd.GetContactById(reservation.ContactID);
            var room = _singletonBd.GetRoomById(reservation.RoomID);
            reservationDTOs.Add(_reservationPostConverter.Convert(reservation, contact, room));
        }
        return reservationDTOs;
    }

    public override async Task<List<ReservationDTO>> GetReservations()
    {
        await Task.Delay(10);
        List<ReservationDTO> result = _singletonBd.GetAllReservations().Select(r =>
        {
            var contact = _singletonBd.GetContactById(r.ContactID);
            var room = _singletonBd.GetRoomById(r.RoomID);
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
                ContactID = reservationDto.ContactID,
                Cancelled = reservationDto.Cancelled,
                ReservationDate = reservationDto.ReservationDate,
                RoomID = reservationDto.RoomID,
                UseDate = reservationDto.UseDate,
            };
            _singletonBd.AddReservation(newReservation);
            if(_singletonBd.GetAllReservations().Contains(newReservation))
                return reservationDto;
            else 
                throw new Exception("Reservation not created");
        }

        throw new Exception("Reservation data not found");
    }

    public override async Task<ReservationDTO> CancelReservation(Guid contactId) //check later
    {
        await Task.Delay(10);
        var reservation = _singletonBd.GetAllReservations().FirstOrDefault(x => x.ContactID == contactId);
        var contact = _singletonBd.GetAllContacts().FirstOrDefault(x => x.ContactID == reservation.ContactID);
        var room = _singletonBd.GetAllRooms().FirstOrDefault(x => x.RoomID == reservation.RoomID);
        if (reservation != null)
        {
            reservation.Cancelled = false;
            _singletonBd.UpdateReservation(reservation);
            if (reservation.Cancelled == false)
                return _reservationPostConverter.Convert(reservation, contact, room);
            else
                throw new Exception("Reservation not cancelled");

        }
        throw new Exception("Reservation not found");
    }
}
