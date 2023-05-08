using GeekBurger.StoreCatalogs.Application.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.StoreCatalogs.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts(
            [FromQuery] string storeName,
            [FromQuery] int userId,
            [FromQuery] string[] restrictions,
            [FromServices] IGetProductService getProductService)
        {
            var products = getProductService.GetProducts(storeName, userId, restrictions);
            return Ok(products);
        }
    }
}
