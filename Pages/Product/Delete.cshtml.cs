using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public DeleteModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.FirstOrDefaultAsync(m => m.id == id);

            if (produit == null)
            {
                return NotFound();
            }
            else
            {
                Produit = produit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.FindAsync(id);
            if (produit != null)
            {
                Produit = produit;
                _context.Produit.Remove(Produit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
