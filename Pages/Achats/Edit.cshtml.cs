using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.Achats
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Achat Achat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achat = await _context.Achat
                .Include(p => p.produit)
                .Include(s => s.utilisateur)
                .FirstOrDefaultAsync(m => m.id == id);

            if (achat == null)
            {
                return NotFound();
            }

            Achat = achat;
            ViewData["idProduit"] = new SelectList(_context.Produit, "id", "libelle");
            ViewData["idUtilisateur"] = new SelectList(_context.Utilisateur, "id", "pseudo");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingAchat = await _context.Achat.AsNoTracking().FirstOrDefaultAsync(a => a.id == Achat.id);
            if (existingAchat == null)
            {
                return NotFound();
            }

            Produit produit = await _context.Produit.FindAsync(Achat.idProduit);
            if (produit == null)
            {
                return NotFound();
            }

            int quantityDifference = Achat.qte - existingAchat.qte;
            Achat.mttJeton = Achat.qte * produit.prixjeton;
            Achat.mttAR = Achat.mttJeton * 100;

            if (produit.stock < quantityDifference)
            {
                throw new Exception("Stock insuffisant pour ce produit!");
            }

            produit.stock -= quantityDifference;

            _context.Attach(Achat).State = EntityState.Modified;
            _context.Attach(produit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchatExists(Achat.id))
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

        private bool AchatExists(int id)
        {
            return _context.Achat.Any(e => e.id == id);
        }
    }
}
