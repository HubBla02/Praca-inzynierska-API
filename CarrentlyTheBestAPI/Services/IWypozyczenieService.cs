using CarrentlyTheBestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Services
{
    public interface IWypozyczenieService
    {
        Task<List<Wypozyczenie>> GetAll(int filtr, int? miesiac, int? rok);
        public IEnumerable<Wypozyczenie> GetByUserEmail(string email);
        Wypozyczenie GetById(int id);
        int UtworzWypozyczenie(Wypozyczenie wypozyczenie);
        bool ZakonczWypozyczenie(int id);
        bool DeleteById(int id);
    }
}
