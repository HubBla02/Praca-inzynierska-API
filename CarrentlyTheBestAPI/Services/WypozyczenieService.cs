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

        public IEnumerable<Wypozyczenie> GetByUserEmail(string email)
        {
            return _dbContext.Wypozyczenia
                .Include(w => w.Pojazd)
                .Where(w => w.UzytkownikEmail == email)
                .ToList();
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

            if (pojazd == null || uzytkownik == null)
            {
                return -1;
            }

            pojazd.Dostepny = false;
            wypozyczenie.DataRozpoczecia = DateTime.SpecifyKind(wypozyczenie.DataRozpoczecia, DateTimeKind.Utc);
            wypozyczenie.DataZakonczenia = DateTime.SpecifyKind(wypozyczenie.DataZakonczenia, DateTimeKind.Utc);

            wypozyczenie.Pojazd = pojazd;
            wypozyczenie.UzytkownikEmail = uzytkownik.Email;
            if (uzytkownik.Znizka == true)
            {
                uzytkownik.Znizka = false;
            }
            _dbContext.Wypozyczenia.Add(wypozyczenie);
            _dbContext.SaveChanges();
            return wypozyczenie.Id;
        }
        public bool DeleteById(int id)
        {
            var wypozyczenie = _dbContext.Wypozyczenia
                             .Include(w => w.Pojazd)
                             .FirstOrDefault(p => p.Id == id);
            if (wypozyczenie == null)
            {
                return false;
            }
            wypozyczenie.Pojazd.Dostepny = true;
            _dbContext.Wypozyczenia.Remove(wypozyczenie);
            _dbContext.SaveChanges();
            return true;
        }
    }
}

