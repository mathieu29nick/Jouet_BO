using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Back_Gestion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Back_Gestion.Models;

namespace Back_Gestion.Pages.Product
{
    public class ImportCSVModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ImportCSVModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload == null || Upload.Length == 0)
            {
                ModelState.AddModelError("Upload", "File is empty.");
                return Page();
            }

            var produits = new List<Produit>();

            try
            {
                using (var reader = new StreamReader(Upload.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var values = line.Split(',');

                        if (values.Length < 7)
                        {
                            ModelState.AddModelError("Upload", "Invalid CSV format.");
                            return Page();
                        }
                        string filePath = values[6];
                        IFormFile formFile = CreateIFormFileFromPath(filePath);
                        string img = null;
                        if (formFile != null)
                        {
                            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                            var extension = Path.GetExtension(formFile.FileName);
                            var imageName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imageName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            img = imageName;
                        }
                        var produitImport = new Produit
                        {
                            libelle = values[0],
                            description = values[1],
                            prixjeton = int.TryParse(values[2], out int prix) ? prix : 0,
                            datepublication = DateTime.TryParseExact(values[3], "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime date) ? date : (DateTime?)null,
                            idCategorie = int.TryParse(values[4], out int idCat) ? idCat : 0,
                            stock = int.TryParse(values[5], out int stock) ? stock : 0,
                            image = img
                        };
                        produits.Add(produitImport);
                    }
                }

                _context.Produit.AddRange(produits);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Upload", $"An error occurred: {ex.Message}");
                return Page();
            }
        }

        public IFormFile CreateIFormFileFromPath(string filePath)
        {
            // Créez un objet Stream pour lire le contenu du fichier
            var stream = new FileStream(filePath, FileMode.Open);

            // Créez un objet IFormFile avec le nom de fichier et le contenu
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream" // Définissez le type de contenu selon votre besoin
            };

            return file;
        }
    }
}
