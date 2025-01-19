using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _OrderRepository;
        public OrderController(OrderRepository OrderRepository)
        {
            this._OrderRepository = OrderRepository;
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            var Orders = _OrderRepository.GetAllOrders();
            return Ok(Orders);
        }

        [HttpPost]
        public IActionResult InsertOrder(OrderModel Order)
        {
            if (Order == null)
            {
                return BadRequest();
            }
            bool isinserted = _OrderRepository.AddOrder(Order);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateOrder(OrderModel Order)
        {
            if (Order == null)
            {
                return BadRequest();
            }
            bool isinserted = _OrderRepository.EditOrder(Order);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteOrder(int OrderID)
        {

            bool isinserted = _OrderRepository.DeleteOrder(OrderID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
