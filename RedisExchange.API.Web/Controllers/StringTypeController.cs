using Microsoft.AspNetCore.Mvc;
using RedisExchange.API.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchange.API.Web.Controllers
{
    public class StringtypeController : Controller
    {
        private readonly RedisService _redisService;
        public StringtypeController(RedisService redisService)
        {
            _redisService = redisService;
        }
        public IActionResult Index()
        {
            var db = _redisService.GetDb(0);
            db.StringSet("name", "Onur Yurt");
            db.StringSet("ziyaretci", 100);
            return View();
        }
    }
}
