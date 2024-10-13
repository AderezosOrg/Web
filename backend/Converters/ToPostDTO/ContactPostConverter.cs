using DTOs.WithId;
using DTOs.WithoutId;
using Entities;
using IConverters;

namespace backend.Converters.ToPostDTO;

public class ContactPostConverter : IConverter1To2<Contact, List<Reservation>, ContactPostDTO>
{
    public ContactPostDTO Convert(Contact contact, List<Reservation> reservations)
    {
        var reservationList = reservations
            .Where(r => r.ContactID == contact.ContactID) 
            .Select(r => r.RoomID) 
            .ToList();

        return new ContactPostDTO
        {
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
            ReservationList = reservationList
        };
    }
}