using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailRepository _OrderDetailRepository;
        public OrderDetailController(OrderDetailRepository OrderDetailRepository)
        {
            this._OrderDetailRepository = OrderDetailRepository;
        }

        [HttpGet]
        public IActionResult GetOrderDetail()
        {
            var OrderDetails = _OrderDetailRepository.GetAllOrderDetails();
            return Ok(OrderDetails);
        }

        [HttpPost]
        public IActionResult InsertOrderDetail(OrderDetailModel OrderDetail)
        {
            if (OrderDetail == null)
            {
                return BadRequest();
            }
            bool isinserted = _OrderDetailRepository.AddOrderDetail(OrderDetail);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateOrderDetail(OrderDetailModel OrderDetail)
        {
            if (OrderDetail == null)
            {
                return BadRequest();
            }
            bool isinserted = _OrderDetailRepository.EditOrderDetail(OrderDetail);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteOrderDetail(int OrderDetailID)
        {

            bool isinserted = _OrderDetailRepository.DeleteOrderDetail(OrderDetailID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

    }
}
