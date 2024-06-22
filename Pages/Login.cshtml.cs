using Back_Gestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Back_Gestion.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public LoginModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        public IActionResult OnGet()
        {
            // Rediriger si déjà connecté
            if (HttpContext.Session.GetString("Token") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
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
            Utilisateur user = await _context.Utilisateur.FirstOrDefaultAsync(u => u.pseudo == Utilisateur.pseudo && u.mdp == Utilisateur.mdp && u.role=="ROLE_ADMIN");
            

            if (user!=null)
            {
                Console.WriteLine("ATO55");
                var token = Utilisateur.mdp+Utilisateur.id+ DateTime.Now.ToString("yyyyMMddHHmmss") + Utilisateur.pseudo;
                HttpContext.Session.SetString("Token", token);
                TempData["Token"] = token;
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError("", "Pseudo ou mot de passe incorrect");
            return Page();
        }
    }
}
