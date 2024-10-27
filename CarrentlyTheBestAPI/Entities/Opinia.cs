namespace CarrentlyTheBestAPI.Entities
{
    public class Opinia
    {
        public int Id { get; set; }
        public int AutorId {  get; set; }
        public virtual Uzytkownik Autor {  get; set; }
        public int Ocena { get; set; }
        public string Opis { get; set; }
    }
}
