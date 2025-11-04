using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    //localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]//api/products
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        //localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
        
        //localhost:5000/api/products/1 => GET
        [HttpGet("{id}")] //{api/[controller]/{id}}
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound(); //StatusCode(404, "Aradiginiz Kaynak Bulunamadi");
            }
            var p = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p); //200 code yani basarili
        }
    }
}