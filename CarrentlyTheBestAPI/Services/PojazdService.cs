using CarrentlyTheBestAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarrentlyTheBestAPI.Services
{
    public class PojazdService : IPojazdService
    {
        private readonly WypozyczenieDbContext _dbContext;
        public PojazdService(WypozyczenieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Pojazd> GetAll()
        {
            var pojazdy = _dbContext.Pojazdy.ToList();
            return pojazdy;
        }

        public Pojazd GetById(int id)
        {
            var pojazd = _dbContext.Pojazdy.FirstOrDefault(p => p.Id == id);
            if (pojazd == null)
            {
                return null;
            }
            return pojazd;
        }

        public int UtworzPojazd(Pojazd pojazd)
        {
            _dbContext.Pojazdy.Add(pojazd);
            _dbContext.SaveChanges();
            return pojazd.Id;
        }
        public bool DeleteById(int id)
        {
            var pojazd = _dbContext.Pojazdy.FirstOrDefault(p => p.Id == id);
            if (pojazd == null)
            {
                return false;
            }
            _dbContext.Pojazdy.Remove(pojazd);
            _dbContext.SaveChanges();
            return true;
        }

        public bool EdytujPojazd(int id, Pojazd zmiany)
        {
            var pojazd = _dbContext.Pojazdy.FirstOrDefault(p => p.Id == id);
            if (pojazd == null)
            {
                return false;
            }
            pojazd.Typ = zmiany.Typ;
            pojazd.Marka = zmiany.Marka;
            pojazd.Model = zmiany.Model;
            pojazd.RodzajSkrzyni = zmiany.RodzajSkrzyni;
            pojazd.RodzajPaliwa = zmiany.RodzajPaliwa;
            pojazd.RokProdukcji = zmiany.RokProdukcji;
            pojazd.Dostepny = zmiany.Dostepny;
            pojazd.CenaK = zmiany.CenaK;
            pojazd.CenaD = zmiany.CenaD;

            _dbContext.Pojazdy.Update(pojazd);
            _dbContext.SaveChanges();

            return true;
        }
    }
} 

