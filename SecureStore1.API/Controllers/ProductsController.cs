using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var x = await _productService.GetAllProductsAsync();
            return Ok(x);
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var x = await _productService.GetProductByIdAsync(id);
            return Ok(x);
        }
    }
}
