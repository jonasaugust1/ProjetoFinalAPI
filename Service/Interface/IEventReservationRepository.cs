using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface IEventReservationRepository
    {
        Task<bool> AddReservation(EventReservationEntity reservation);
        Task<IEnumerable<EventReservationEntity>> GetReservationByNameAndEventTitle(string name, string eventTitle);
        Task<bool> DeleteReservation(long id);
        Task<bool> UpdateReservationAmount(long id, int amount);

        Task<bool> ValidateEventStatus(long eventId);
    }
}