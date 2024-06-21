using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public CreateModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["idCategorie"] = new SelectList(_context.Categorie, "id", "libelle");
            return Page();
        }

        [BindProperty]
        public Produit Produit { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["idCategorie"] = new SelectList(_context.Categorie, "id", "libelle");
                Console.WriteLine("Erreur de validation :");

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Count > 0)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"{key}: {error.ErrorMessage}");
                        }
                    }
                }

                return Page();
            }

            if (Produit.ImageUpload != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Produit.ImageUpload.FileName);
                var extension = Path.GetExtension(Produit.ImageUpload.FileName);
                var imageName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Produit.ImageUpload.CopyToAsync(stream);
                }

                Produit.image = imageName;
            }

            _context.Produit.Add(Produit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
