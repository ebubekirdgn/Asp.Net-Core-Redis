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
            if (!_memoryCache.TryGetValue("time", out string timecache))
            {
                MemoryCacheEntryOptions options = new();
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);
                options.Priority = CacheItemPriority.High; // Ram dolarsa neye göre sileceğini belirleriz.
                options.RegisterPostEvictionCallback((key,value,reason,state) =>
                {
                    _memoryCache.Set("callback", $"{key}-> {value} => {reason}");
                });

                _memoryCache.Set<string>("time", DateTime.Now.ToString(),options);
            }

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.Remove("Time");
            _memoryCache.TryGetValue<string>("time",out string timecache);
            _memoryCache.TryGetValue<string>("callback", out string callback);

            ViewBag.Time = timecache;
            ViewBag.CallBack = callback;
            return View();
        }
    }
}