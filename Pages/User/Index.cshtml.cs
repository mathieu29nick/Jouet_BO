using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Back_Gestion.Models;
using Back_Gestion.Services;
using System.Configuration;

namespace Back_Gestion.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public IndexModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Pagination<Utilisateur> Utilisateur {  set; get; }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Utilisateur> query = _context.Utilisateur.Include(s => s.Jeton).AsNoTracking().AsQueryable();
            Utilisateur = await Pagination<Utilisateur>.CreateAsync(
                query,pageIndex ?? 1 , 2);
        }
    }
}
