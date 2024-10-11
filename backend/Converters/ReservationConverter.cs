using DTOs;
using Entities;
using IConverters;

namespace Converters
{
    public class ReservationConverter : IConverter1To3<Reservation, Contact, Room, ReservationDTO>
    {
        public ReservationDTO Convert(Reservation reservation, Contact contact, Room room)
        {
            return new ReservationDTO
            {
                ContactID = reservation.ContactID,
                RoomID = reservation.RoomID,
                ReservationDate = reservation.ReservationDate,
                UseDate = reservation.UseDate,
                UserPhoneNumber = contact.PhoneNumber,
                UserEmail = contact.Email,
                RoomCode = room.Code,
                RoomFloorNumber = room.FloorNumber,
                RoomAvailable = room.Available
            };
        }
    }
}
