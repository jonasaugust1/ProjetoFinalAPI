using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface ICityEventService
    {
        Task<bool> AddEvent(CityEventDto cityEvent);
        Task<IEnumerable<CityEventDto>> GetEventByLocalAndDate(string local, DateTime date);
        Task<IEnumerable<CityEventDto>> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime date);
        Task<IEnumerable<CityEventDto>> GetEventByTitle(string title);
        Task<bool> UpdateEvent(CityEventDto cityEvent, long id);
        Task<bool> DeleteOrInativateEvent(long id);
    }
}