using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CityEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        private IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("ConsultaReserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<EventReservationEntity> GetReservation(string name, string eventTitle)
        {
            return Ok(_eventReservationService.GetReservation(name, eventTitle));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<ActionResult<EventReservationEntity>> AddReservation(EventReservationDto entity)
        {
            if (!await _eventReservationService.AddReservation(entity))
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventReservationEntity>> UpdateReservationAmount(long id, int amount)
        {
            if (!await _eventReservationService.UpdateReservationAmount(id, amount))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteReservation([FromQuery] long id)
        {
            if (!await _eventReservationService.DeleteReservation(id))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}