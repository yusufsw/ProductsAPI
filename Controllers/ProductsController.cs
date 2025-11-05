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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity); //StatusCode(201) = CreatedAction
        }

        //localhost:5000/api/products/1 => PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if (id != entity.ProductId)
            {
                return BadRequest();//400'lu bir hata yani sen servisi yanlis kullandin. serviste hata yok demektir
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound(); //hatayi loglamak lazim aslinda
            }

            return NoContent(); //204 surum kodu. her seyin normal oldugunu soyler. geriye bir deger dondurmez 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent(); // basarili islem. geriye donus yapilmaz
        }
    }
}