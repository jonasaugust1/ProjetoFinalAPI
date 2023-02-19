using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CityEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventsController : ControllerBase
    {
        private ICityEventService _cityEventService;

        public CityEventsController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("ConsultaPorPrecoEData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityEventsEntity> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime date)
        {
            return Ok(_cityEventService.GetEventByPriceAndDate(minPrice, maxPrice, date));
        }

        [HttpGet("ConsultaPorLocalEData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityEventsEntity> GetEventByLocalAndDate(string local, DateTime data)
        {
            return Ok(_cityEventService.GetEventByLocalAndDate(local, data));
        }

        [HttpGet("ConsultaPorTitulo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityEventsEntity> GetEventByTitle(string titulo)
        {
            return Ok(_cityEventService.GetEventByTitle(titulo));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityEventsEntity>> AddEvent(CityEventDto entity)
        {
            if (!await _cityEventService.AddEvent(entity))
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityEventsEntity>> UpdateEvent(CityEventDto entity, long id)
        {
            if (!await _cityEventService.UpdateEvent(entity, id))
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrInativateEvent([FromQuery] long id)
        {

            if (!await _cityEventService.DeleteOrInativateEvent(id))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}