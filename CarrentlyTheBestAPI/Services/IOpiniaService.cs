using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IOpiniaService
    {
        IEnumerable<Opinia> GetAll();
        int UtworzOpinie(Opinia opinia);
        bool DeleteById(int id);
    }
}
