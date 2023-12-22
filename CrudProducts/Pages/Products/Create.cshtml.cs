using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrudProducts.Data;
using CrudProducts.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace CrudProducts.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly CrudProducts.Data.CrudProductsContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateModel(CrudProducts.Data.CrudProductsContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

       

        [BindProperty]
        public Product Product { get; set; }
        // Property to represent the uploaded image file
        [BindProperty]
        public IFormFile ImageFile { get; set; }


        [BindProperty]
        public int Category { get; set; }
        public SelectList CategoryList { get; set; }

        public IActionResult OnGet()
        {
            // Chargez la liste des catégories dans la méthode OnGet
            CategoryList = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }



        [HttpPost]
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Generate a unique filename using a timestamp
                var fileName = DateTime.Now.Ticks + Path.GetExtension(ImageFile.FileName);

                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Ensure the uploads folder exists
                Directory.CreateDirectory(uploadsFolder);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Save the file path in your database
                Product.imageUrl = "/uploads/" + fileName;
                // Update the path as per your project structure

            }

            Product.CategoryId = Category;
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
