using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrudProducts.Data;
using CrudProducts.Model;
using System;

namespace CrudProducts.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly CrudProductsContext _context;

        public IndexModel(CrudProductsContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        [BindProperty(SupportsGet = true)]
        public string prd { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? minPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? maxPrice { get; set; }

        // Ajout d'une nouvelle propriété pour les catégories
        public IList<Category> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string selectedCategory { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                IQueryable<Product> productsQuery = _context.Product;

                if (!string.IsNullOrEmpty(prd))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(prd));
                }

                if (minPrice.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.Price <= maxPrice.Value);
                }
                if (!string.IsNullOrEmpty(selectedCategory))
                {
                    int selectedCategoryId;
                    if (int.TryParse(selectedCategory, out selectedCategoryId))
                    {
                        Console.WriteLine($"Selected Category Id: {selectedCategoryId}");
                        productsQuery = productsQuery.Where(p => p.CategoryId == selectedCategoryId);
                    }
                }
                Categories = await _context.Category.ToListAsync();
                Product = await productsQuery.ToListAsync();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

    }
}
