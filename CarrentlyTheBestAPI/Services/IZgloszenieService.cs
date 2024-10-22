using CarrentlyTheBestAPI.DTO;
using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IZgloszenieService
    {
        IEnumerable<Zgloszenie> GetAll();
        Zgloszenie GetById(int id);
        int UtworzZgloszenie(Zgloszenie zgloszenie);
        public bool ZmienStatus(int id);
        public IEnumerable<Zgloszenie> GetByUserEmail(string email);
        public bool DodajOdpowiedz(int id, string odpowiedz);
        bool UsunZgloszenie(int id);
    }
}
