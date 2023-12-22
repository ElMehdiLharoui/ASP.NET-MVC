using CrudProducts.Data;
using CrudProducts.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProducts.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly CrudProductsContext _context;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(CrudProductsContext context, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            _logger.LogInformation("les information");
            return _context.Category.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            _logger.LogInformation($"Obtenir la catégorie avec l'ID {id}");
            var category = _context.Category.FirstOrDefault(e => e.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public ActionResult<Category> CreateCategory(Category category)
        {
            _logger.LogInformation("Créer une nouvelle catégorie");

            _context.Category.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            _logger.LogInformation($"Mettre à jour la catégorie avec l'ID {id}");

            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            _logger.LogInformation($"Supprimer la catégorie avec l'ID {id}");

            var category = _context.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}
