namespace Back_Gestion.Models
{
    public class Jeton
    {
        public int id { get; set; }
        public int idUtilisateur { get; set; }
        public int nombreJeton { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }
}
