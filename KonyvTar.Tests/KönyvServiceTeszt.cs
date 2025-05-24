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
        private K�nyvt�rAdatb�zis GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<K�nyvt�rAdatb�zis>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new K�nyvt�rAdatb�zis(options);
        }

        [Fact]
        public async Task UjKonyv_Hozzaadasa_MegjelenikListaban()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new K�nyvService(context);
            var konyv = new K�nyv { C�m = "Teszt k�nyv", Szerz� = "Tesztel�", Kiad� = "Teszt Kiad�", Kiad�s�ve = 2020 };

            // Act
            await service.L�trehoz�sAsync(konyv);
            var eredmeny = await service.List�z�sAsync();

            // Assert
            Assert.Single(eredmeny);
            Assert.Equal("Teszt k�nyv", eredmeny[0].C�m);
        }
    }
}
