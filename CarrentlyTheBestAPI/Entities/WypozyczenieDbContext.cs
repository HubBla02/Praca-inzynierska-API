using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CarrentlyTheBestAPI.Entities
{
    public class WypozyczenieDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public WypozyczenieDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WypozyczalniaDB"));
            options.UseLazyLoadingProxies();
        }

        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
        public DbSet<Pojazd> Pojazdy { get; set; }
        public DbSet<Rola> Role {  get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Zgloszenie> Zgloszenia { get; set; }
        public DbSet<Opinia> Opinie { get; set; }
    }
}
