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
    public class DetailsModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public DetailsModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.Include(p => p.categorie).FirstOrDefaultAsync(m => m.id == id);
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
    }
}
