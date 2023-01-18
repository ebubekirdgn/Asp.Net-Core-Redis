using IDistrubutedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

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

            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
            _distrubutedCache.Set("product:1",byteProduct);
            //await _distrubutedCache.SetStringAsync("product:1",jsonProduct,distributedCacheEntryOptions);
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
            Byte[] byteProduct = _distrubutedCache.Get("product:1");
            string jsonProduct = Encoding.UTF8.GetString(byteProduct);

            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);
            ViewBag.ProductName = p;  
            return View();  
        }


        public IActionResult Remove()
        {
            _distrubutedCache.Remove("name");
            return View();
        }
    
        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/araba.jpg");

            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            _distrubutedCache.Set("resim",imageByte);
            return View();  
        }
        public IActionResult ImageUrl()
        {
            byte[] resimbyte = _distrubutedCache.Get("resim");
            return File(resimbyte,"image/jpg");
        }
    
    }
}