﻿namespace Back_Gestion.Models
{
    public class Categorie
    {
        public int id {  get; set; }
        public string libelle {  get; set; }

        public ICollection<Produit>? produit { get; set; }
    }
}
