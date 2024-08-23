using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IWypozyczenieService
    {
        IEnumerable<Wypozyczenie> GetAll();
        Wypozyczenie GetById(int id);
        int UtworzWypozyczenie(Wypozyczenie wypozyczenie);
        bool DeleteById(int id);
    }
}
