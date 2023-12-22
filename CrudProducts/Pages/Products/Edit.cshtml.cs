using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudProducts.Data;
using CrudProducts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CrudProducts.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly CrudProducts.Data.CrudProductsContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(CrudProducts.Data.CrudProductsContext context,IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public SelectList CategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            // Chargez la liste des catégories
            CategoryList = new SelectList(await _context.Category.ToListAsync(), "Id", "Name", Product.CategoryId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Rechargez la liste des catégories en cas d'échec de la validation
                CategoryList = new SelectList(await _context.Category.ToListAsync(), "Id", "Name", Product.CategoryId);
                return Page();
            }

            // Si une nouvelle image est fournie
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Supprimez l'ancienne image si elle existe
                if (!string.IsNullOrEmpty(Product.imageUrl))
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", Product.imageUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Générez un nom de fichier unique en utilisant un timestamp
                var fileName = DateTime.Now.Ticks + Path.GetExtension(ImageFile.FileName);

                // Chemin où sauvegarder l'image
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Assurez-vous que le dossier d'uploads existe
                Directory.CreateDirectory(uploadsFolder);

                // Sauvegardez le fichier sur le serveur
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Mise à jour du chemin de l'image dans la base de données
                Product.imageUrl = "/uploads/" + fileName;
            }

            _context.Update(Product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
