using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_Gestion.Models
{
    public class Produit
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Le champ Libelle est requis.")]
        [Display(Name = "Libelle")]
        public string libelle { get; set; }
        [Required(ErrorMessage = "Le champ desription est requis.")]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required(ErrorMessage = "Le champ Jeton est requis.")]
        [Display(Name = "Jeton")]
        public int prixjeton {  get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de Publication")]
        public DateTime datepublication { get; set; }
        [Required(ErrorMessage = "Le champ Categorie est requis.")]
        [Display(Name = "Categorie")]
        public int idCategorie { get; set; }
        [Required(ErrorMessage = "Le champ Stock est requis.")]
        [Display(Name = "Quantité/Stock")]
        public int stock {  get; set; }
        public string? image {  get; set; }
        public Categorie? categorie { get; set; }
        [NotMapped]
        public IFormFile? ImageUpload { get; set; }
    }
}
