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
    public class IndexModel : PageModel
    {
        private readonly Back_Gestion.Services.ApplicationDbContext _context;

        public IndexModel(Back_Gestion.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Pagination<Produit> Produit { set; get; }
        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Produit> query = _context.Produit.Include(s => s.categorie).AsNoTracking().AsQueryable();
            Produit = await Pagination<Produit>.CreateAsync(
                query, pageIndex ?? 1, 3);
        }
    }
}
