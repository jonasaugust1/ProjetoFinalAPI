using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface ICityEventRepository
    {
        Task<bool> AddEvent(CityEventsEntity cityEvent);

        Task<bool> UpdateEvent(CityEventsEntity cityEvent, long id);

        Task<bool> DeleteEvent(long id);

        Task<bool> InativateEvent(long id);

        Task<bool> GetEventsReservation(long eventId);

        Task<List<CityEventsEntity>> GetEventByTitle(string title);

        Task<List<CityEventsEntity>> GetEventByLocalAndDate(string local, DateTime date);

        Task<List<CityEventsEntity>> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime date);
    }
}