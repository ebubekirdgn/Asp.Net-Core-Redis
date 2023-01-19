using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        RedisService _redisService;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
        }
        public IActionResult Index()
        {
            var db = _redisService.GetDb(5);
            //SET
            db.StringSet("name", "Ebubekir");
            db.StringSet("ziyaretci", 100);
            db.StringIncrement("ziyaretci",20);
            var data = db.StringDecrementAsync("ziyaretci",15).Result;
            ////GET
            //string value_get = db.StringGet("name");
            ////APPEND
            //db.StringAppend("name", "Dogan");
            //string value_append = db.StringGet("name");
            ////INCR
            //db.StringSet("count", 1);
            //long value_count = db.StringIncrement("count");
            ////GETRANGE
            //string value_getrange = db.StringGetRange("name", 1, 2);

            return View();
        }
    }
}
