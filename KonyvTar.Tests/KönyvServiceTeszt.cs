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
            var service = new KönyvService(context);
            var konyv = new Könyv { Cím = "Teszt könyv", Szerzõ = "Tesztelõ", Kiadó = "Teszt Kiadó", KiadásÉve = 2020 };

            // Act
            await service.LétrehozásAsync(konyv);
            var eredmeny = await service.ListázásAsync();

            // Assert
            Assert.Single(eredmeny);
            Assert.Equal("Teszt könyv", eredmeny[0].Cím);
        }
    }
}
