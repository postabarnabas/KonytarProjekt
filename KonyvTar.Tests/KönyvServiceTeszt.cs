using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Posta_Barnabas_Projekt.Data;
using Posta_Barnabas_Projekt.Models;
using Posta_Barnabas_Projekt.Services;

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
            var service = new KonyvService(context);
            var konyv = new Konyv { Cim = "Teszt k�nyv", Szerzo = "Tesztel�", Kiado = "Teszt Kiad�", Kiadaseve = 2020 };

            // Act
            await service.HozzaadasAsync(konyv);
            var eredmeny = await service.ListazAsync();

            // Assert
            Assert.Single(eredmeny);
            Assert.Equal("Teszt k�nyv", eredmeny[0].Cim);
        }
    }
}
