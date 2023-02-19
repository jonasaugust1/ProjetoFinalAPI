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
    public class EventReservationService : IEventReservationService
    {
        private IEventReservationRepository _repository;
        private IMapper _mapper;

        public EventReservationService(IEventReservationRepository rep, IMapper mapper)
        {
            _repository = rep;
            _mapper = mapper;
        }

        public async Task<bool> AddReservation(EventReservationDto reservation)
        {
            bool status = await _repository.ValidateEventStatus(reservation.IdEvent);
            if (status)
            {
                EventReservationEntity entity = _mapper.Map<EventReservationEntity>(reservation);
                return await _repository.AddReservation(entity);
            }
            return false;
        }

        public async Task<bool> DeleteReservation(long id)
        {
            return await _repository.DeleteReservation(id);
        }

        public async Task<IEnumerable<EventReservationDto>> GetReservation(string name, string eventTitle)
        {
            IEnumerable<EventReservationEntity> entity = await _repository.GetReservationByNameAndEventTitle(name, eventTitle);
            if (entity == null)
            {
                return null;
            }
            IEnumerable<EventReservationDto> reservationDto = _mapper.Map<IEnumerable<EventReservationDto>>(entity);
            return reservationDto;
        }

        public async Task<bool> UpdateReservationAmount(long id, int amount)
        {
            return await _repository.UpdateReservationAmount(id, amount);
        }
    }
}
