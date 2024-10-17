using backend.DTOs.WithId;
using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Entities;
using Converters.ToDTO;
using DTOs.WithId;
using backend.MyHappyBD;
using Db;

namespace backend.Services;

public class ReservationService : IReservationService,
    IGetAllElementsService<ReservationDTO>
{
    private ReservationDAO _reservationDao;
    private ContactDAO _contactDao;
    private RoomDAO _roomDao;
    private ReservationConverter _reservationConverter = new ReservationConverter();


    public ReservationService(ReservationDAO reservationDao, ContactDAO contactDao, RoomDAO roomDao)
    {
        _reservationDao = reservationDao;
        _contactDao = contactDao;
        _roomDao = roomDao;
    }

    public async Task<List<ReservationDTO>> GetReservationsByContactId(Guid contactId)
    {
        await Task.Delay(20);
        var reservations = _reservationDao.GetReservationsByContactId(contactId);
        
        var reservationDTOs = new List<ReservationDTO>();
        foreach (var reservation in reservations)
        {
            var contact = _contactDao.Read(reservation.ContactID);
            var room = _roomDao.Read(reservation.RoomID);
            reservationDTOs.Add(_reservationConverter.Convert(reservation, contact, room));
        }
        return reservationDTOs;

    }

    public async Task<List<ReservationDTO>> GetReservationsByRoomId(Guid roomId)
    {
        await Task.Delay(20);
        var reservations = _reservationDao.GetReservationsByRoomId(roomId);
        
        var reservationDTOs = new List<ReservationDTO>();
        foreach (var reservation in reservations)
        {
            var contact = _contactDao.Read(reservation.ContactID);
            var room = _roomDao.Read(reservation.RoomID);
            reservationDTOs.Add(_reservationConverter.Convert(reservation, contact, room));
        }
        return reservationDTOs;
    }

    public async Task<List<ReservationDTO>> GetAllElements()
    {
        await Task.Delay(10);
        List<ReservationDTO> result = _reservationDao.ReadAll().Select(r =>
        {
            var contact = _contactDao.Read(r.ContactID);
            var room = _roomDao.Read(r.RoomID);
            return _reservationConverter.Convert(r, contact, room);
        }).ToList();
        
        return result;
    }

    public async Task<List<ReservationDTO>> CreateReservation(ReservationPostDTO[] reservationPostDto)
    {
        await Task.Delay(100);
        List<ReservationDTO> newReservations = new List<ReservationDTO>(); 
        foreach (ReservationPostDTO postDto in reservationPostDto)
        {
            if (postDto != null)
            {
                var newReservation = new Reservation
                {
                    ContactID = postDto.ContactId,
                    Cancelled = false,
                    ReservationDate = postDto.ReservationDate,
                    RoomID = postDto.RoomId,
                    UseDate = postDto.UseDate,
                };
                _reservationDao.Create(newReservation);
                newReservations.Add(_reservationConverter.Convert(postDto, _contactDao.Read(postDto.ContactId), _roomDao.Read(postDto.RoomId)));
            }
        }

        return newReservations;

    }

    public async Task<ReservationDTO> CancelReservation(CancelReservationDTO cancelReservationDto) //check later
    {
        await Task.Delay(10);
        var reservations = _reservationDao.GetReservationsByContactId(cancelReservationDto.ContactID).Where(reservation1 => reservation1.RoomID == cancelReservationDto.RoomID);
        var reservation = reservations.FirstOrDefault();
        var contact = _contactDao.Read(reservation.ContactID);
        var room = _roomDao.Read(reservation.RoomID);
        if (reservation != null)
        {
            reservation.Cancelled = false;
            _reservationDao.Update(reservation);
            if (reservation.Cancelled == false)
                return _reservationConverter.Convert(reservation, contact, room);
            else
                throw new Exception("Reservation not cancelled");

        }
        throw new Exception("Reservation not found");
    }
}
