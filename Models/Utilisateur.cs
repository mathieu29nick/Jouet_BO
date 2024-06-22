using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Back_Gestion.Models
{
    public class Utilisateur
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Le champ pseudo est requis.")]
        [Display(Name = "Email/Pseudo")]
        public string pseudo { get; set; }

        [Required(ErrorMessage = "Le champ mdp est requis.")]
        [Display(Name = "Mot de passe")]
        public string mdp { get; set; }

        [Required(ErrorMessage = "Le champ role est requis.")]
        [Display(Name = "Rôle de l'utilisateur")]
        public string role { get; set; }
       
        public Jeton? Jeton { get; set; }
        public ICollection<Achat>? Achat { get; set; }
    }
}
