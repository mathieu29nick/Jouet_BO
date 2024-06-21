using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;

namespace Back_Gestion.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public IndexModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Categorie> Categorie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Categorie = await _context.Categorie.ToListAsync();
        }
    }
}
