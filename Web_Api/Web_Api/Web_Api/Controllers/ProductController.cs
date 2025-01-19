using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Model;


namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _ProductRepository;
        public ProductController(ProductRepository ProductRepository)
        {
            this._ProductRepository = ProductRepository;
        }
        [HttpGet]
        public IActionResult GetProduct()
        {
            var Products = _ProductRepository.GetAllProducts();
            return Ok(Products);
        }

        [HttpPost]
        public IActionResult InsertProduct(ProductModel Product)
        {
            if (Product == null)
            {
                return BadRequest();
            }
            bool isinserted = _ProductRepository.AddProduct(Product);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductModel Product)
        {
            if (Product == null)
            {
                return BadRequest();
            }
            bool isinserted = _ProductRepository.EditProduct(Product);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int ProductID)
        {

            bool isinserted = _ProductRepository.DeleteProduct(ProductID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
