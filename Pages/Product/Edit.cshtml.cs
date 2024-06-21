using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Produit Produit { get; set; }

        public SelectList Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produit = await _context.Produit.FirstOrDefaultAsync(m => m.id == id);

            if (Produit == null)
            {
                return NotFound();
            }

            Categories = new SelectList(_context.Categorie, "id", "libelle");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingProduct = await _context.Produit.FindAsync(Produit.id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            if (Produit.ImageUpload != null)
            {
                if (!string.IsNullOrEmpty(existingProduct.image))
                {
                    string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", existingProduct.image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                existingProduct.image = await SaveImageAsync(Produit.ImageUpload);
            }

            existingProduct.libelle = Produit.libelle;
            existingProduct.description = Produit.description;
            existingProduct.prixjeton = Produit.prixjeton;
            existingProduct.datepublication = Produit.datepublication;
            existingProduct.idCategorie = Produit.idCategorie;
            existingProduct.stock = Produit.stock;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitExists(Produit.id))
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

        private bool ProduitExists(int id)
        {
            return _context.Produit.Any(e => e.id == id);
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string uniqueFileName = null;

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
