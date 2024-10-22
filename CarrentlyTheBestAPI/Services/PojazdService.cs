using CarrentlyTheBestAPI.DTO;
using CarrentlyTheBestAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;

namespace CarrentlyTheBestAPI.Services
{
    public class PojazdService : IPojazdService
    {
        private readonly WypozyczenieDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        public PojazdService(WypozyczenieDbContext dbContext, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
        public IEnumerable<Pojazd> GetAll()
        {
            var pojazdy = _dbContext.Pojazdy
                                    .OrderBy(p => p.Id)
                                    .ToList();
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

        public Pojazd UtworzPojazd(Pojazd pojazd)
        {   
            _dbContext.Pojazdy.Add(pojazd);
            _dbContext.SaveChanges();
            return pojazd;
        }
        public bool DeleteById(int id)
        {
            var pojazd = _dbContext.Pojazdy.FirstOrDefault(p => p.Id == id);
            if (pojazd == null)
            {
                return false;
            }
            var usunZdjecie = _fileService.UsunZdjecie(pojazd.SciezkaDoZdjecia);
            if (!usunZdjecie)
            {
                return false;
            }
            _dbContext.Pojazdy.Remove(pojazd);
            _dbContext.SaveChanges();
            return true;
        }


        public bool DodajZdjecie(int id, string sciezkaZdjecia)
        {
            var pojazd = _dbContext.Pojazdy.Find(id);
            if (pojazd == null)
                return false;

            pojazd.SciezkaDoZdjecia = sciezkaZdjecia;
            _dbContext.SaveChanges();
            return true;
        }

        public bool UsunZdjecie(int id)
        {
            var pojazd = _dbContext.Pojazdy.Find(id);
            if (pojazd == null)
                return false;

            pojazd.SciezkaDoZdjecia = null;
            _dbContext.SaveChanges();
            return true;
        }

        public bool EdytujPojazd(int id, PojazdDTO zmiany)
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

