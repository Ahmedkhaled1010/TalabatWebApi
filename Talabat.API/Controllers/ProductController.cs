using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Reposatory;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiBaseController
    {
        private readonly IGenericRepository<Product> productRebo;

        public ProductController(IGenericRepository<Product> ProductRebo)
        {
            productRebo = ProductRebo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Products = await productRebo.GetAllAsync();
            //OkObjectResult result = new OkObjectResult(Products);
            //return result;
            return Ok(Products);
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productRebo.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
