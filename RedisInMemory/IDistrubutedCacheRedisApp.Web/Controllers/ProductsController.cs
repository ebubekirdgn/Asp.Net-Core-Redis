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

        public IActionResult Index()
        {
            return View();
        }
    }
}
