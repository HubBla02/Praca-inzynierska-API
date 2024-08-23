using CarrentlyTheBestAPI.Entities;

namespace CarrentlyTheBestAPI.Services
{
    public interface IKontoService
    {
        string generujJWT(Login dto);
        void RejestracjaU(Rejestracja uzytkownik);
    }
}