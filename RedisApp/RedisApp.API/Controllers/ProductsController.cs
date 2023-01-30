using Microsoft.AspNetCore.Mvc;
using RedisApp.API.Model;
using RedisApp.API.Repository;
using RedisApp.Cache;

namespace RedisApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService;

        public ProductsController(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository = productRepository;
            _redisService=  redisService;

            var db = _redisService.GetDb(0);
            db.StringSet("name","ebubekir");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAsync());
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            return Ok(await _productRepository.CreateAsync(product));
        }
    }
}