using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace testFileCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            _logger = logger;
            _easyCachingProviderFactory = easyCachingProviderFactory;
        }

        [HttpGet]
        public Test Get()
        {
            var cache = _easyCachingProviderFactory.GetCachingProvider("mm");

            var test = cache.Get<Test>("test");

            if (test.HasValue)
            {
                return test.Value;
            }

            var cacheValue = new Test
            {
                Now = DateTime.Now
            };

            cache.Set("test", cacheValue, TimeSpan.FromSeconds(10));

            return cacheValue;
        }
    }

    public class Test
    {
        public DateTime Now { get; set; }
    }
}