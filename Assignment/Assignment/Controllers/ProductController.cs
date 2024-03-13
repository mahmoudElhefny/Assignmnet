using Assignment.Domain.Contracts.IServices;
using Assignment.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
     
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProcutAsync([FromForm] productVM model)
        {

            var respone = await _productService.Add(model);
            if (respone.IsSucceded)
                return Ok(respone);
            return BadRequest(respone);
        }
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, [FromForm] productVM model)
        {

            var response = await _productService.update(id, model);
            if (response.IsSucceded)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var respone = await _productService.GetAllProducts();
            return respone.IsSucceded ? Ok(respone) : BadRequest(respone);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            var respone = await _productService.GetById(id);
            if (respone.IsSucceded)
                return Ok(respone);

            return BadRequest(respone);
        }
        [HttpDelete("Delete")]
        public async Task<Response<bool>> Delete([FromQuery] int id)
        {
            var respone = await _productService.Remove(id);
            return respone;
        }
    }
}
