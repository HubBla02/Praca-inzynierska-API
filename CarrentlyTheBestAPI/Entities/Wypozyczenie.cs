﻿namespace CarrentlyTheBestAPI.Entities
{
    public class Wypozyczenie
    {
        public int Id { get; set; }
        public string UzytkownikEmail { get; set; }
        public int PojazdId { get; set; }
        public virtual Pojazd Pojazd {  get; set; }
        public float Cena { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public bool CzyZakonczone { get; set; } = false;
    }
}
