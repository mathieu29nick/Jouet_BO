using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "ROLE_ADMIN", Text = "Admin" },
            new SelectListItem { Value = "ROLE_VISITEUR", Text = "Simple Utilisateur" },
        };
        public CreateModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; } = default!;

        [BindProperty]
        public Jeton Jeton { get; set; } = new Jeton();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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

            _context.Utilisateur.Add(Utilisateur);
            await _context.SaveChangesAsync();

            // Add Jeton
            Jeton.idUtilisateur = Utilisateur.id; 
            Jeton.nombreJeton = 300;
            _context.Jeton.Add(Jeton);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
