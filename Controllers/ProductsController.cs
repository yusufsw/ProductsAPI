using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    //localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]//api/products
    public class ProductsController : ControllerBase
    {
        private static List<Product>? _products;

        public ProductsController()
        {

        }

        //localhost:5000/api/products => GET
        [HttpGet]
        public IActionResult GetProducts()
        {
            if(_products == null)
            {
                return NotFound();
            }
            return Ok(_products);
        }
        
        //localhost:5000/api/products/1 => GET
        [HttpGet("{id}")] //{api/[controller]/{id}}
        public IActionResult GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound(); //StatusCode(404, "Aradiginiz Kaynak Bulunamadi");
            }
            var p = _products?.FirstOrDefault(i => i.ProductId == id);

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p); //200 code yani basarili
        }
    }
}