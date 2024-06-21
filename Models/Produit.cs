using System.ComponentModel.DataAnnotations.Schema;

namespace Back_Gestion.Models
{
    public class Produit
    {
        public int id { get; set; }
        public string libelle { get; set; }
        public string description { get; set; }
        public int prixjeton {  get; set; }
        public DateTime datepublication { get; set; }
        public int idCategorie { get; set; }
        public int stock {  get; set; }
        public string? image {  get; set; }
        public Categorie? categorie { get; set; }
        [NotMapped]
        public IFormFile? ImageUpload { get; set; }
    }
}
