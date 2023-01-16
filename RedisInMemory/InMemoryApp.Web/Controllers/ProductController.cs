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
            _memoryCache.Set<string>("Time",DateTime.Now.ToString());
            return View();
        }

        public IActionResult Show()
        {
          ViewBag.Time=  _memoryCache.Get<string>("Time");
            return View();
        }
    }
}
