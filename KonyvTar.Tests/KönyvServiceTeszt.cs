using Moq;
using Xunit;
using Posta_Barnabas_Projekt.Modellek;
using Posta_Barnabas_Projekt.Data;
using Posta_Barnabas_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Konyvtar.Tests
{
    public class KonyvServiceTests
    {
        private KönyvtárAdatbázis GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<KönyvtárAdatbázis>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new KönyvtárAdatbázis(options);
        }

        [Fact]
        public async Task UjKonyv_Hozzaadasa_MegjelenikListaban()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new KonyvService(context);
            var konyv = new Konyv { Cim = "Teszt könyv", Szerzo = "Tesztelõ", Kiado = "Teszt Kiadó", Kiadaseve = 2020 };

            // Act
            await service.HozzaadasAsync(konyv);
            var eredmeny = await service.ListazAsync();

            // Assert
            Assert.Single(eredmeny);
            Assert.Equal("Teszt könyv", eredmeny[0].Cim);
        }
    }
}
