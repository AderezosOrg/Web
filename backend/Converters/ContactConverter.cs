using DTOs;
using Entities;
using IConverters;
using System.Collections.Generic;
using System.Linq;

namespace Converters
{
    public class ContactConverter : IConverter1To2<Contact, List<Reservation>, ContactDTO>
    {
        public ContactDTO Convert(Contact contact, List<Reservation> reservations)
        {
            var reservationList = reservations
                .Where(r => r.UserID == contact.ContactID) 
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
