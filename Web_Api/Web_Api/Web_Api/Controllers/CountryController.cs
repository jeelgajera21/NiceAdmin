using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Web_Api.Data;
using Web_Api.Model;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;
        public CountryController(CountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }
        [HttpGet]
        public IActionResult GetCountry() {
            var Countries = _countryRepository.GetAllCountries();
            return Ok(Countries);
        }

        [HttpPost]
        public IActionResult InsertCountry([FromForm]CountryModel country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            bool isinserted = _countryRepository.AddCountry(country);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateCountry([FromForm] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            bool isinserted = _countryRepository.EditCountry(country);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteCountry([FromForm] int CountryID)
        {

            bool isinserted = _countryRepository.DeleteCountry(CountryID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryByID(int id)
        {
            var country = _countryRepository.GetCountryByID(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

    }
}
