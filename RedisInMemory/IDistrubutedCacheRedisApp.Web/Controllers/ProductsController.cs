using IDistrubutedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistrubutedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDistributedCache _distrubutedCache;

        public ProductsController(IDistributedCache distrubutedCache)
        {
            _distrubutedCache = distrubutedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions distributedCacheEntryOptions = new();
            distributedCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            Product product = new Product {Id =1,Name ="Kalem",Price = 1200 };
            
            string jsonProduct = JsonConvert.SerializeObject(product);

            await _distrubutedCache.SetStringAsync("product:1",jsonProduct,distributedCacheEntryOptions);
            //_distrubutedCache.SetString("name","Ebubekir",distributedCacheEntryOptions);
            //await _distrubutedCache.SetStringAsync("surname","dogan",distributedCacheEntryOptions);
            return View();
        }
        public IActionResult Show()
        {
            //string name = _distrubutedCache.GetString("name");
            //string surname = _distrubutedCache.GetString("surname");
            //ViewBag.Name = name;
            //ViewBag.Surname = surname;

            string jsonProduct = _distrubutedCache.GetString("product:1");
            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);
            ViewBag.ProductName = p;  
            return View();  
        }


        public IActionResult Remove()
        {
            _distrubutedCache.Remove("name");
            return View();
        }
    }
}