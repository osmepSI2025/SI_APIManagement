using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SME_API_Apimanagement.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Authorize] // Require authentication
        public IActionResult GetProducts()
        {
            var products = new[]
            {
            new { Id = 1, Name = "Product A" },
            new { Id = 2, Name = "Product B" }
        };
            return Ok(products);
        }
    }
}
