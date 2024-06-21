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
    public class DeleteModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public DeleteModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur.FirstOrDefaultAsync(m => m.id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur
                                            .Include(u => u.Jeton)
                                            .FirstOrDefaultAsync(u => u.id == id);

            if (utilisateur != null)
            {
                if (utilisateur.Jeton != null)
                {
                    _context.Jeton.Remove(utilisateur.Jeton);
                }

                _context.Utilisateur.Remove(utilisateur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }
}
