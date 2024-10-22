using System.ComponentModel.DataAnnotations;

namespace CarrentlyTheBestAPI.Entities
{
    public class Zgloszenie
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Tytul { get; set; }
        [Required]
        public string Tresc { get; set; }
        public string? Odpowiedz { get; set; }
        public bool CzyZamkniete { get; set; }
    }
}
