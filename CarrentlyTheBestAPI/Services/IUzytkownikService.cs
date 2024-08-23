using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IUzytkownikService
    {
        IEnumerable<Uzytkownik> GetAll();
        Uzytkownik GetById(int id);
        public bool EdytujUzytkownika(int id, Uzytkownik zmiany);
        bool DeleteById(int id);
    }
}

