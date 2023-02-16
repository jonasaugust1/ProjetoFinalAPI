using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CityEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventsController : ControllerBase
    {
        private ICityEventRepository _cityEventRepository;

        public CityEventsController(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        [HttpGet("ConsultaPorPrecoEData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityEventsEntity> GetConsultaPorPrecoData(double precoMin, double precoMax, DateTime data)
        {
            return Ok(_cityEventRepository.ConsultaPorPrecoEData(precoMin, precoMax, data));
        }

        [HttpGet("ConsultaPorLocalEData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityEventsEntity> GetConsultaPorLocalEData(string local, DateTime data)
        {
            return Ok(_cityEventRepository.ConsultaPorLocalEData(local, data));
        }

        [HttpGet("ConsultaPorTitulo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityEventsEntity> GetConsultaPorTitulo(string titulo)
        {
            return Ok(_cityEventRepository.ConsultaPorTitulo(titulo));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEventsEntity> Inserir(CityEventsEntity entity)
        {
            if (!_cityEventRepository.AdicionarEvento(entity))
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEventsEntity> EditarEvento(CityEventsEntity entity, int id)
        {
            if (!_cityEventRepository.EditarEvento(entity, id))
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        // Pensar e construir o delete.

        //[HttpDelete]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<CityEventsEntity> 
    }
}