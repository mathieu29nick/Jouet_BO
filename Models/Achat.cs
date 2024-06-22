using System.ComponentModel.DataAnnotations;

namespace Back_Gestion.Models
{
    public class Achat
    {
        public int id { get; set; }
        [Display(Name = "Utilisateur")]
        public int idUtilisateur {  get; set; }
        [Display(Name = "Produit")]
        public int idProduit {  get; set; }
        [Display(Name = "Quantité")]
        public int qte { get; set; }
        [Display(Name = "Montant en Ariary")]
        public double? mttAR {  get; set; }
        [Display(Name = "Montant en Jeton")]
        public int? mttJeton { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date d'achat")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateAchat { get; set; }
        [Display(Name = "Produit")]
        public Produit? produit { get; set; }
        [Display(Name = "Utilisateur")]
        public Utilisateur? utilisateur { get; set;}
    }
}
