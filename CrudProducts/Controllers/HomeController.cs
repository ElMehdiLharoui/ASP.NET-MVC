using CrudProducts.Data;
using CrudProducts.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace CrudProducts.Controllers
{
    [Route("api/Produit")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class HomeController : ControllerBase
    {
        private readonly CrudProductsContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(CrudProductsContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProduits()
        {
            _logger.LogInformation("les information");
            return _context.Product.ToList();
        }
    }
}
