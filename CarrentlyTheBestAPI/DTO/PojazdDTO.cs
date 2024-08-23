namespace CarrentlyTheBestAPI.DTO
{
    public class PojazdDTO
    {
        public string Typ { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string RodzajSkrzyni { get; set; }
        public string RodzajPaliwa { get; set; }
        public int RokProdukcji { get; set; }
        public bool Dostepny { get; set; }
        public float CenaK { get; set; }
        public float CenaD { get; set; }
        public IFormFile Zdjecie { get; set; }
    }
}
