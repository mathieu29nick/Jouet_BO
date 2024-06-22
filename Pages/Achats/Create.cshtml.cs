using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Back_Gestion.Models;
using Back_Gestion.Services;
using Microsoft.EntityFrameworkCore;

namespace Back_Gestion.Pages.Achats
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["idProduit"] = new SelectList(_context.Produit, "id", "libelle");
            ViewData["idUtilisateur"] = new SelectList(_context.Utilisateur, "id", "pseudo");
            return Page();
        }

        [BindProperty]
        public Achat Achat { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var produit = await _context.Produit.FindAsync(Achat.idProduit);
            if (produit == null)
            {
                return NotFound();
            }

            if (produit.stock < Achat.qte)
            {
                throw new Exception("Stock insuffisant pour ce produit!");
            }

            Achat.mttJeton = Achat.qte * produit.prixjeton;
            Achat.mttAR = Achat.mttJeton * 100;

            produit.stock -= Achat.qte;

            _context.Achat.Add(Achat);
            _context.Attach(produit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
