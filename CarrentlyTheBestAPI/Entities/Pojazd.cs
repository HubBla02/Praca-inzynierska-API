using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarrentlyTheBestAPI.Entities
{
    public class Pojazd
    {
        public int Id { get; set; }
        public string? Typ { get; set; }
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public string? RodzajSkrzyni { get; set; }
        public string? RodzajPaliwa  { get; set; }
        public int RokProdukcji { get; set; }
        public bool Dostepny {  get; set; }
        public float CenaK { get; set; }
        public float CenaD { get;  set; }
        public string? SciezkaDoZdjecia { get; set; }
    }
}
