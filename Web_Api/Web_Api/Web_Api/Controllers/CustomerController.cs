using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;


namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _CustomerRepository;
        public CustomerController(CustomerRepository CustomerRepository)
        {
            this._CustomerRepository = CustomerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var Customers = _CustomerRepository.GetAllCustomer();
            return Ok(Customers);
        }

        [HttpPost]
        public IActionResult InsertCustomer(CustomerModel Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }
            bool isinserted = _CustomerRepository.AddCustomer(Customer);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateCustomer(CustomerModel Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }
            bool isinserted = _CustomerRepository.EditCustomer(Customer);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int CustomerID)
        {

            bool isinserted = _CustomerRepository.DeleteCustomer(CustomerID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpGet("{id}")]
        public IActionResult GetCityByID(int id)
        {
            var customer = _CustomerRepository.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
