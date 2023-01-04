using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

               options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
         //      options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.High;
                _memoryCache.Set<string>("Zaman", DateTime.Now.ToString(),options);
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"{key}->{ value}=>sebep: { reason}");
            });

                Product p = new Product { ID = 1, Name = "Kalem", Price = 10 };
                _memoryCache.Set<Product>("product:1", p);
            _memoryCache.Set<double>("money", 100.99);
            
            

            return View();
        }
        public IActionResult Show()
        {
            _memoryCache.TryGetValue("Zaman", out string zamancache);
            _memoryCache.TryGetValue("Callback", out string callback);
            ViewBag.Callback = callback;
            ViewBag.Zaman = zamancache;
            ViewBag.Product = _memoryCache.Get<Product>("product:1");
            return View();
        }
    }
}
