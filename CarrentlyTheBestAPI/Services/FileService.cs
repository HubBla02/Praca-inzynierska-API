

using Microsoft.AspNetCore.Hosting;

namespace CarrentlyTheBestAPI.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;
        public FileService(IWebHostEnvironment _environment)
        {
            this.environment = _environment;
        }

        public bool UsunZdjecie(string sciezka)
        {
            try
            {
                if (sciezka.StartsWith("/"))
                {
                    sciezka = sciezka.Substring(1);
                }

                var pelnaSciezka = Path.Combine(environment.WebRootPath, sciezka);
                if (File.Exists(pelnaSciezka))
                {
                    File.Delete(pelnaSciezka);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public (int, string) ZapiszZdjecie(IFormFile plik)
        {
            if (plik.Length > 0)
            {
                var folderPath = Path.Combine(environment.WebRootPath, "zdjeciaPojazdow");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = $"{Guid.NewGuid()}_{plik.FileName}";
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    plik.CopyTo(stream);
                }

                return (1, $"/zdjeciaPojazdow/{fileName}");
            }
            return (0, string.Empty);
        }
    }
}

