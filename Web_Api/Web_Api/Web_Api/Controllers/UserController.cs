using Web_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;



namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _UserRepository;
        public UserController(UserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }
        [HttpGet]
        public IActionResult GetState()
        {
            var State = _UserRepository.GetAllUser();
            return Ok(State);
        }

        [HttpPost]
        public IActionResult InsertUser(UserModel User)
        {
            if (User == null)
            {
                return BadRequest();
            }
            bool isinserted = _UserRepository.AddUser(User);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateUser(UserModel User)
        {
            if (User == null)
            {
                return BadRequest();
            }
            bool isinserted = _UserRepository.EditUser(User);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteState(int UserID)
        {

            bool isinserted = _UserRepository.DeleteUser(UserID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
