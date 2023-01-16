using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            ////1. YOL
            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("Time")))
            //{
            //    _memoryCache.Set<string>("Time", DateTime.Now.ToString());
            //}
            //2.YOL
            if (!_memoryCache.TryGetValue("Time", out string timecache))
            {
                MemoryCacheEntryOptions options = new();
                //options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);


                _memoryCache.Set<string>("Time", DateTime.Now.ToString(),options);
            }

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.Remove("Time");
            _memoryCache.TryGetValue<string>("Time",out string timecache);

            ViewBag.Time = timecache;
            return View();
        }
    }
}