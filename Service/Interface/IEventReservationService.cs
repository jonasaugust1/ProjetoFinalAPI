using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface IEventReservationService
    {
        Task<bool> AddReservation(EventReservationDto reservation);
        Task<IEnumerable<EventReservationDto>> GetReservation(string name, string eventTitle);
        Task<bool> DeleteReservation(long id);
        Task<bool> UpdateReservationAmount(long id, int amount);
    }
}