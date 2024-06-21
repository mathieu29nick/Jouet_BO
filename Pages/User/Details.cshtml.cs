using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public DetailsModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Utilisateur Utilisateur { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur.Include(s => s.Jeton).AsNoTracking().FirstOrDefaultAsync(m => m.id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            else
            {
                Utilisateur = utilisateur;
            }
            return Page();
        }
    }
}
