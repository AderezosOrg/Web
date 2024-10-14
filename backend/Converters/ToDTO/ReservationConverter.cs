using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class ReservationConverter : IConverter1To3<Reservation, Contact, Room, ReservationDTO>
    {
        public ReservationDTO Convert(Reservation reservation, Contact contact, Room room)
        {
            Console.WriteLine(contact.PhoneNumber);
            Console.WriteLine(room.Code);
            return new ReservationDTO()
            {
                ContactID = reservation.ContactID,
                RoomID = reservation.RoomID,
                ReservationDate = reservation.ReservationDate,
                UseDate = reservation.UseDate,
                UserPhoneNumber = contact.PhoneNumber,
                UserEmail = contact.Email,
                RoomCode = room.Code,
                RoomFloorNumber = room.FloorNumber,
                PricePerNight = room.PricePerNight,
                Cancelled = reservation.Cancelled
            };
        }
    }
}
