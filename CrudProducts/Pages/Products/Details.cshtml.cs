using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrudProducts.Data;
using CrudProducts.Model;

namespace CrudProducts.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly CrudProducts.Data.CrudProductsContext _context;

        public DetailsModel(CrudProducts.Data.CrudProductsContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public string CategoryName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            var category = await _context.Category.FindAsync(Product.CategoryId);
            CategoryName = category?.Name;

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
