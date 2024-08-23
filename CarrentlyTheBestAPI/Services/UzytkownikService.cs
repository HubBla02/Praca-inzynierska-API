using CarrentlyTheBestAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Services
{
    public class UzytkownikService : IUzytkownikService
    {
        private readonly WypozyczenieDbContext _dbContext;
        public UzytkownikService(WypozyczenieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Uzytkownik> GetAll()
        {
            var uzytkownicy = _dbContext.Uzytkownicy.ToList();
            return uzytkownicy;
        }

        public Uzytkownik GetById(int id)
        {
            var uzytkownik = _dbContext.Uzytkownicy
                                         .FirstOrDefault(w => w.Id == id);
            if (uzytkownik == null)
            {
                return null;
            }
            return uzytkownik;
        }

        public bool DeleteById(int id)
        {
            var uzytkownik = _dbContext.Uzytkownicy.FirstOrDefault(p => p.Id == id);
            if (uzytkownik == null)
            {
                return false;
            }
            _dbContext.Uzytkownicy.Remove(uzytkownik);
            _dbContext.SaveChanges();
            return true;
        }

        public bool EdytujUzytkownika(int id, Uzytkownik zmiany)
        {
            var uzytkownik = _dbContext.Uzytkownicy.FirstOrDefault(p => p.Id == id);
            if (uzytkownik == null)
            {
                return false;
            }
            uzytkownik.Email = zmiany.Email;
            uzytkownik.Imie = zmiany.Imie;
            uzytkownik.Nazwisko = zmiany.Nazwisko;
            uzytkownik.DataUrodzenia = zmiany.DataUrodzenia;
            uzytkownik.RolaId = zmiany.RolaId;
            uzytkownik.Rola = zmiany.Rola;
            uzytkownik.CzyZablokowany = zmiany.CzyZablokowany;
            uzytkownik.CzyTrzezwy = zmiany.CzyTrzezwy;

            _dbContext.Uzytkownicy.Update(uzytkownik);
            _dbContext.SaveChanges();

            return true;
        }
    }
}

