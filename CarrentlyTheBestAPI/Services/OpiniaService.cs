using CarrentlyTheBestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Services
{
    public class OpiniaService : IOpiniaService
    {
        private readonly WypozyczenieDbContext _dbContext;
        public OpiniaService(WypozyczenieDbContext context) { _dbContext = context; }

        public IEnumerable<Opinia> GetAll()
        {
            var opinie = _dbContext.Opinie.Include(o => o.Autor).ToList();
            return opinie;
        }

        public int UtworzOpinie(Opinia opinia)
        {
            var autor = _dbContext.Uzytkownicy.Find(opinia.AutorId);

            if (autor == null)
            {
                return -1;
            }

            opinia.AutorId = autor.Id;
            autor.Znizka = true;
            autor.CzyOpinia = true;

            opinia.Autor = autor;
            _dbContext.Opinie.Add(opinia);
            _dbContext.SaveChanges();
            return opinia.Id;
        }

        public bool DeleteById(int id)
        {
            var opinia = _dbContext.Opinie
                 .FirstOrDefault(p => p.Id == id);
            if (opinia == null)
            {
                return false;
            }
            _dbContext.Opinie.Remove(opinia);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
