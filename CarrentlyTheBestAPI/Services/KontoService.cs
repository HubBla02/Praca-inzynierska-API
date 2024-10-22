using CarrentlyTheBestAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarrentlyTheBestAPI.Services
{
    public class KontoService : IKontoService
    {
        private readonly WypozyczenieDbContext _context;
        private readonly IPasswordHasher<Uzytkownik> _hasher;
        private readonly AuthenticationSettings _settings;

        public KontoService(WypozyczenieDbContext context, IPasswordHasher<Uzytkownik> hasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _hasher = hasher;
            _settings = authenticationSettings;
        }

        public string generujJWT(Login dto)
        {
            var uzytkownik = _context.Uzytkownicy
                .Include(u => u.Rola)
                .FirstOrDefault(x => x.Email == dto.Email);
            if (uzytkownik is null) {
                throw new Exception("Niepoprawny email lub haslo.");
            }

            var wynik = _hasher.VerifyHashedPassword(uzytkownik, uzytkownik.HasloHash, dto.Haslo);
            if (wynik == PasswordVerificationResult.Failed)
            {
                throw new Exception("Niepoprawny email lub haslo.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, uzytkownik.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{uzytkownik.Imie} {uzytkownik.Nazwisko}"),
                new Claim(ClaimTypes.Role, $"{uzytkownik.Rola.Nazwa}"),
                new Claim(ClaimTypes.Email, $"{uzytkownik.Email}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_settings.JwtExpireDays);
            var token = new JwtSecurityToken(_settings.JwtIssuer, _settings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RejestracjaU(Rejestracja uzytkownik)
        {
            var nowyU = new Uzytkownik()
            {
                Imie = uzytkownik.Imie,
                Nazwisko = uzytkownik.Nazwisko,
                Email = uzytkownik.Email,
                DataUrodzenia = uzytkownik.DataUrodzenia,
                RolaId = uzytkownik.RolaId,
            };
            var hashed = _hasher.HashPassword(nowyU, uzytkownik.Haslo);
            nowyU.HasloHash = hashed;
            _context.Uzytkownicy.Add(nowyU);
            _context.SaveChanges();
        }
    }
}
