using GeekBurger.StoreCatalogs.Application.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.StoreCatalogs.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] string storeName,
            [FromQuery] int userId,
            [FromQuery] string[] restrictions,
            [FromServices] IGetProductService getProductService)
        {
            try
            {
                return Ok((await getProductService.GetProducts(storeName, userId, restrictions)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
