using System.ComponentModel.DataAnnotations;

namespace Back_Gestion.Models
{
    public class Categorie
    {
        public int id {  get; set; }
        [Required(ErrorMessage = "Le champ Libelle est requis.")]
        [Display(Name = "Libelle")]
        public string libelle {  get; set; }

        public ICollection<Produit>? produit { get; set; }
    }
}
