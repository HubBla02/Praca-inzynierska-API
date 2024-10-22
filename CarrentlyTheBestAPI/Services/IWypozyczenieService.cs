using CarrentlyTheBestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Services
{
    public interface IWypozyczenieService
    {
        IEnumerable<Wypozyczenie> GetAll();
        public IEnumerable<Wypozyczenie> GetByUserEmail(string email);
        Wypozyczenie GetById(int id);
        int UtworzWypozyczenie(Wypozyczenie wypozyczenie);
        bool DeleteById(int id);
    }
}
