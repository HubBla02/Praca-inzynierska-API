namespace CarrentlyTheBestAPI.Services
{
    public interface IFileService
    {
        public (int, string) ZapiszZdjecie(IFormFile plik);
        public bool UsunZdjecie(string sciezka);
    }
}
