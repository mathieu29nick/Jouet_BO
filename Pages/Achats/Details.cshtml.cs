using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.Achats
{
    public class DetailsModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public DetailsModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Achat Achat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achat = await _context.Achat.Include(p=>p.produit).Include(s=>s.utilisateur).FirstOrDefaultAsync(m => m.id == id);
            if (achat == null)
            {
                return NotFound();
            }
            else
            {
                Achat = achat;
            }
            return Page();
        }
    }
}
