using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CityController : ControllerBase
    {
        private readonly CityRepository _CityRepository;
        public CityController(CityRepository CityRepository)
        {
            this._CityRepository = CityRepository;
        }
        
        [HttpGet]
        public IActionResult GetCities()
        {
            var Cities = _CityRepository.GetAllCities();
            return Ok(Cities);
        }

        [HttpPost]
        public IActionResult InsertCity(CityModel City)
        {
            if (City == null)
            {
                return BadRequest();
            }
            bool isinserted = _CityRepository.AddCites(City);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateCity(CityModel City)
        {
            if (City == null)
            {
                return BadRequest();
            }
            bool isinserted = _CityRepository.EditCites(City);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteCity(int CityID)
        {

            bool isinserted = _CityRepository.DeleteCites(CityID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpGet("{id}")]
        public IActionResult GetCityByID(int id)
        {
            var city = _CityRepository.GetCityByID(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
