using AutoMapper;
using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Service
{
    public class CityEventService : ICityEventService
    {
        private ICityEventRepository _repository;
        private IMapper _mapper;

        public CityEventService(ICityEventRepository rep, IMapper mapper)
        {
            _repository = rep;
            _mapper = mapper;
        }

        public async Task<bool> AddEvent(CityEventDto cityEvent)
        {
            CityEventsEntity cityEventsEntity = _mapper.Map<CityEventsEntity>(cityEvent);
            return await _repository.AddEvent(cityEventsEntity);
        }

        public async Task<bool> DeleteOrInativateEvent(long id)
        {
            bool reservationAmount = await _repository.GetEventsReservation(id);

            if (reservationAmount != false)
            {
                return await _repository.DeleteEvent(id);
            }
            return await _repository.InativateEvent(id);
        }

        public async Task<IEnumerable<CityEventDto>> GetEventByLocalAndData(string local, DateTime date)
        {
            IEnumerable<CityEventsEntity> entity = await _repository.GetEventByLocalAndDate(local, date);
            if (entity == null)
            {
                return null;
            }

            IEnumerable<CityEventDto> eventDto = _mapper.Map<IEnumerable<CityEventDto>>(entity);
            return eventDto;
        }

        public async Task<IEnumerable<CityEventDto>> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime date)
        {
            IEnumerable<CityEventsEntity> entity = await _repository.GeteventByPriceAndDate(minPrice, maxPrice, date);
            if (entity == null)
            {
                return null;
            }
            IEnumerable<CityEventDto> eventDto = _mapper.Map<IEnumerable<CityEventDto>>(entity);
            return eventDto;
        }

        public async Task<IEnumerable<CityEventDto>> GetEventByTitle(string title)
        {
            IEnumerable<CityEventsEntity> entity = await _repository.GetEventByTitle(title);
            if (entity == null)
            {
                return null;
            }
            IEnumerable<CityEventDto> eventDto = _mapper.Map<IEnumerable<CityEventDto>>(entity);
            return eventDto;
        }

        public async Task<bool> UpdateEvent(CityEventDto cityEvent, long id)
        {
            CityEventsEntity entity = _mapper.Map<CityEventsEntity>(cityEvent);
            return await _repository.UpdateEvent(entity, id);
        }
    }
}