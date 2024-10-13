using DTOs.WithId;
using Entities;
using IConverters;

namespace Converters.ToDTO
{
    public class ContactConverter : IConverter1To2<Contact, List<Reservation>, ContactDTO>
    {
        public ContactDTO Convert(Contact contact, List<Reservation> reservations)
        {
            var reservationList = reservations
                .Where(r => r.ContactID == contact.ContactID) 
                .Select(r => r.RoomID) 
                .ToList();

            return new ContactDTO
            {
                ContactID = contact.ContactID,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                ReservationList = reservationList
            };
        }
    }
}
