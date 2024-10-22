using CarrentlyTheBestAPI.DTO;
using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IPojazdService
    {
        IEnumerable<Pojazd> GetAll();
        Pojazd GetById(int id);
        Pojazd UtworzPojazd(Pojazd pojazd);
        public bool EdytujPojazd(int id, PojazdDTO zmiany);
        bool DodajZdjecie(int id, string sciezkaZdjecia);
        bool UsunZdjecie(int id);
        bool DeleteById(int id);
    }
}