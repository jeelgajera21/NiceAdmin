using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _StateRepository;
        public StateController(StateRepository StateRepository)
        {
            this._StateRepository = StateRepository;
        }
        [HttpGet]
        public IActionResult GetState()
        {
            var State = _StateRepository.GetAllState();
            return Ok(State);
        }

        [HttpGet("{id}")]
        public IActionResult GetStateByID(int id)
        {
            var city = _StateRepository.GetStateByID(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        public IActionResult InsertState(StateModel State)
        {
            if (State == null)
            {
                return BadRequest();
            }
            bool isinserted = _StateRepository.AddState(State);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateState(StateModel State)
        {
            if (State == null)
            {
                return BadRequest();
            }
            bool isinserted = _StateRepository.EditState(State);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteState(int StateID)
        {

            bool isinserted = _StateRepository.DeleteState(StateID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
   /*     [HttpGet]
        public IActionResult StateByPk(int StateID)
        {
            var State = _StateRepository.StateByPk(StateID);
            return Ok(State);
        }*/
    }
}
