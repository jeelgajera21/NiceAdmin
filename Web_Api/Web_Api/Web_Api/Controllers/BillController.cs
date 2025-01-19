using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillRepository _BillRepository;
        public BillController(BillRepository billRepository)
        {
            this._BillRepository = billRepository;
        }

        [HttpGet]
        public IActionResult GetBill()
        {   
            var Bills = _BillRepository.GetAllBills();
            return Ok(Bills);
        }

        [HttpPost]
        public IActionResult InsertBill(BillsModel  Bill)
        {
            if (Bill == null)
            {
                return BadRequest();
            }
            bool isinserted = _BillRepository.AddBill(Bill);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateBill(BillsModel Bill)
        {
            if (Bill == null)
            {
                return BadRequest();
            }
            bool isinserted = _BillRepository.EditBill(Bill);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteBill(int BillID)
        {

            bool isinserted = _BillRepository.DeleteBill(BillID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryByID(int id)
        {
            var bill = _BillRepository.GetBillByID(id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }

    }
}
