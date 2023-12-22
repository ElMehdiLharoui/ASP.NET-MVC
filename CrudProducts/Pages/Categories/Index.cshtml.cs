using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrudProducts.Data;
using CrudProducts.Model;

namespace CrudProducts.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CrudProducts.Data.CrudProductsContext _context;

        public IndexModel(CrudProducts.Data.CrudProductsContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
