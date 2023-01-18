using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
            _distrubutedCache.SetString("name","Ebubekir",distributedCacheEntryOptions);
            await _distrubutedCache.SetStringAsync("surname","dogan",distributedCacheEntryOptions);
            return View();
        }
        public IActionResult Show()
        {
            string name = _distrubutedCache.GetString("name");
            string surname = _distrubutedCache.GetString("surname");
            ViewBag.Name = name;
            ViewBag.Surname = surname;
            return View();  
        }


        public IActionResult Remove()
        {
            _distrubutedCache.Remove("name");
            return View();
        }
    }
}