using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IPojazdService
    {
        IEnumerable<Pojazd> GetAll();
        Pojazd GetById(int id);
        int UtworzPojazd(Pojazd pojazd);
        public bool EdytujPojazd(int id, Pojazd zmiany);
        bool DeleteById(int id);
    }
}