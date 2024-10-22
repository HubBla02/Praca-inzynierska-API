using CarrentlyTheBestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Services
{
    public class ZgloszenieService : IZgloszenieService
    {
        private readonly WypozyczenieDbContext _dbContext;
        public ZgloszenieService(WypozyczenieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Zgloszenie> GetAll()
        {
            var zgloszenia = _dbContext.Zgloszenia.OrderBy(p => p.Id).ToList();
            return zgloszenia;
        }

        public Zgloszenie GetById(int id)
        {
            var zgloszenie = _dbContext.Zgloszenia.FirstOrDefault(w => w.Id == id);
            if (zgloszenie == null)
            {
                return null;
            }
            return zgloszenie;
        }


        public int UtworzZgloszenie(Zgloszenie zgloszenie)
        {
            var uzytkownik = _dbContext.Uzytkownicy.FirstOrDefault(u => u.Email == zgloszenie.Email);

            if (uzytkownik == null)
            {
                return -1;
            }
            zgloszenie.Email = uzytkownik.Email;
            zgloszenie.CzyZamkniete = false;
            _dbContext.Zgloszenia.Add(zgloszenie);
            _dbContext.SaveChanges();
            return zgloszenie.Id;
        }

        public bool UsunZgloszenie(int id)
        {
            var zgloszenie = _dbContext.Zgloszenia.FirstOrDefault(p => p.Id == id);
            if (zgloszenie == null)
            {
                return false;
            }
            _dbContext.Zgloszenia.Remove(zgloszenie);
            _dbContext.SaveChanges();
            return true;
        }
        public bool ZmienStatus(int id)
        {
            var zgloszenie = _dbContext.Zgloszenia.FirstOrDefault(p => p.Id == id);
            if (zgloszenie == null)
            {
                return false;
            }
            zgloszenie.CzyZamkniete = !zgloszenie.CzyZamkniete;
            _dbContext.SaveChanges();

            return true;
        }

        public bool DodajOdpowiedz(int id, string odpowiedz)
        {
            var zgloszenie = _dbContext.Zgloszenia.FirstOrDefault(z => z.Id == id);
            if (zgloszenie == null)
            {
                return false;
            }
            zgloszenie.Odpowiedz = odpowiedz;
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Zgloszenie> GetByUserEmail(string email)
        {
            return _dbContext.Zgloszenia.Where(z => z.Email == email).OrderBy(p => p.Id).ToList();
        }
    }
}

