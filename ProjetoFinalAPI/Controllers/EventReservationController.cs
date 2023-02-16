using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CityEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        private IEventReservationRepository _eventReservationRepository;

        public EventReservationController(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        [HttpGet("ConsultaReserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<EventReservationEntity> GetConsultaReserva(string name, string eventTitle)
        {
            return Ok(_eventReservationRepository.ConsultaReserva(name, eventTitle));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservationEntity> Inserir(EventReservationEntity entity, long eventId)
        {
            if (!_eventReservationRepository.AdicionarReserva(entity, eventId))
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservationEntity> EditarEvento(long id, int amount)
        {
            if (!_eventReservationRepository.EditarQuantidadeReserva(id, amount))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}