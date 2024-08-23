using CarrentlyTheBestAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Services
{
    public class WypozyczenieService : IWypozyczenieService
    {
        private readonly WypozyczenieDbContext _dbContext;
        public WypozyczenieService(WypozyczenieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Wypozyczenie> GetAll()
        {
            var wypozyczenia = _dbContext.Wypozyczenia.ToList();
            return wypozyczenia;
        }

        public Wypozyczenie GetById(int id)
        {
            var wypozyczenie = _dbContext.Wypozyczenia
                                         .Include(w => w.Pojazd)
                                         .FirstOrDefault(w => w.Id == id);
            if (wypozyczenie == null)
            {
                return null;
            }
            return wypozyczenie;
        }

        public int UtworzWypozyczenie(Wypozyczenie wypozyczenie)
        {
            var pojazd = _dbContext.Pojazdy.Find(wypozyczenie.PojazdId);
            var uzytkownik = _dbContext.Uzytkownicy.FirstOrDefault(u => u.Email == wypozyczenie.UzytkownikEmail);
            wypozyczenie.Pojazd = pojazd;
            wypozyczenie.UzytkownikEmail = uzytkownik.Email;
            _dbContext.Wypozyczenia.Add(wypozyczenie);
            _dbContext.SaveChanges();
            return wypozyczenie.Id;
        }
        public bool DeleteById(int id)
        {
            var wypozyczenie = _dbContext.Wypozyczenia.FirstOrDefault(p => p.Id == id);
            if (wypozyczenie == null)
            {
                return false;
            }
            _dbContext.Wypozyczenia.Remove(wypozyczenie);
            _dbContext.SaveChanges();
            return true;
        }
    }
}

