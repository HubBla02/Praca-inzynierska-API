using Microsoft.AspNetCore.Authentication;

namespace CarrentlyTheBestAPI.Entities
{
    public class Uzytkownik
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }   
        public string HasloHash { get; set; }
        public int RolaId { get; set; }
        public bool CzyTrzezwy { get; set; } = false;
        public bool CzyZablokowany { get; set; } = false;
        public virtual Rola Rola { get; set; }
    }
}
