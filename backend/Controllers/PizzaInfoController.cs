using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Serilog;
using Serilog.Context;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaInfoController : ControllerBase
    {
        private static readonly PizzaInfo[] TheMenu = new[]
        {
            new PizzaInfo { PizzaName = "The Mighty Meatball", Ingredients = "Meatballs and cheese", Cost = 40, InStock = "yes"},
            new PizzaInfo { PizzaName = "Crab Apple", Ingredients = "Dungeness crab and apples", Cost = 35, InStock = "no"},
            new PizzaInfo { PizzaName = "Forest Floor", Ingredients = "Mushrooms, rutabagas, and walnuts", Cost = 20, InStock = "yes"},
            new PizzaInfo { PizzaName = "Don't At Me", Ingredients = "Pineapple, Canadian bacon, jalapeños", Cost = 25, InStock = "yes"},
            new PizzaInfo { PizzaName = "Vanilla", Ingredients = "Sausage and pepperoni", Cost = 15, InStock = "no"},
            new PizzaInfo { PizzaName = "Spice Coming At Ya", Ingredients = "Peppers, chili sauce, spicy andouille", Cost = 50, InStock = "yes"}
        };

        private readonly ILogger<PizzaInfoController> _logger;

        public PizzaInfoController(ILogger<PizzaInfoController> logger)
        {
            _logger = logger;
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Http("http://elk-stack-logstash-1:5000", queueLimitBytes: null)
                .Enrich.WithProperty("IndexName", "myapp")
                .CreateLogger();
            
        }

        [HttpGet]
        public IEnumerable<PizzaInfo> Get()
        {
            {
                _logger.LogInformation("Executing GET action for PizzaInfoController");
                _logger.LogWarning("This is a warning message");
            }

            return TheMenu;
        }
    }

    public class PizzaInfo
    {
        public string PizzaName { get; set; }
        public string Ingredients { get; set; }
        public int Cost { get; set; }
        public string InStock { get; set; }
    }
}
