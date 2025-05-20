using Microsoft.EntityFrameworkCore;
using Posta_Barnabas_Projekt.Modellek;
namespace Posta_Barnabas_Projekt.Data
{
    public class KönyvtárAdatbázis : DbContext
    {
        public KönyvtárAdatbázis(DbContextOptions<KönyvtárAdatbázis> opciók) : base(opciók) { }
        
        public DbSet<Könyv> Könyvek {  get; set; }
        public DbSet<Olvasó> Olvasók { get; set; }
        public DbSet<Kölcsönzés> Kölcsönzések { get; set; }
    }
}
