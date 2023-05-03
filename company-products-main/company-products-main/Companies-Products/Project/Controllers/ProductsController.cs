using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data;
using Project.Data.DTOs.Product;
using Project.Data.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsConttroller : ControllerBase
    {
        private AppDbContext _appDbContext;
        public ProductsConttroller(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //Krijo nje API endpoint per te marre te dhenat nga DB
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var allProducts = _appDbContext.Products.ToList();

            return Ok(allProducts);
        }

        //Krijo nje API endpoint per te marre te dhenat nga DB
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] PostProductDTO payload)
        {
            Product newProduct = new Product()
            {
                ID = payload.ID,
                Name = payload.Name,
                Code = payload.Code,
                Type = payload.Type,
                DateCreated = DateTime.UtcNow
            };

            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();

            return Ok("Produkti u krijua me sukses!");
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] PutProductDTO payload, int id)
        {
            //1. Duke perdour ID marrim te dhenat nga databaza
            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);

            //2. Perditesojme Productin e DB me te dhenat e payload-it
            if (product == null)
                return NotFound();

            product.ID = payload.ID;
            product.Name = payload.Name;
            product.Code = payload.Code;
            product.Type = payload.Type;

            //3. Ruhen te dhenat ne database
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();

            return Ok($"Produkti me id = {id} u fshi me sukses!");
        }
    }
}