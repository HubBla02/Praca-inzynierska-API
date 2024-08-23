using System.ComponentModel.DataAnnotations;

namespace CarrentlyTheBestAPI.Entities
{
    public class Rejestracja
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string PowtorzHaslo { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string Imie {  get; set; }
        public string Nazwisko { get; set; }
        public int RolaId { get; set; } = 1;

    }
}
