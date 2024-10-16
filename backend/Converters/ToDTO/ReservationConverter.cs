using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class ReservationConverter : IConverter1To3<Reservation, Contact, Room, ReservationDTO>, IConverter1To3<ReservationPostDTO, Contact, Room, ReservationDTO>
    {
        public ReservationDTO Convert(Reservation reservation, Contact contact, Room room)
        {
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

        public ReservationDTO Convert(ReservationPostDTO input1, Contact input2, Room input3)
        {
            return new ReservationDTO()
            {
                ContactID = input1.ContactId,
                PricePerNight = input3.PricePerNight,
                ReservationDate = input1.ReservationDate,
                UseDate = input1.UseDate,
                RoomCode = input3.Code,
                RoomFloorNumber = input3.FloorNumber,
                RoomID = input1.RoomId,
                UserEmail = input2.Email,
                UserPhoneNumber = input2.PhoneNumber
            };
        }
    }
}
