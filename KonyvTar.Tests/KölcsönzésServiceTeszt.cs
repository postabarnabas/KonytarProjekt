using Moq;
using Xunit;
using Posta_Barnabas_Projekt.Models;
using Posta_Barnabas_Projekt.Data;
using Posta_Barnabas_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

public class KolcsonzesServiceTests
{
    private async Task<KönyvtárAdatbázis> GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<KönyvtárAdatbázis>()
            .UseInMemoryDatabase(databaseName: "KolcsonzesTestDb")
            .Options;
        var db = new KönyvtárAdatbázis(options);
        await db.Database.EnsureCreatedAsync();
        return db;
    }

    [Fact]
    public async Task KolcsonzesAsync_AddsKolcsonzes()
    {
        var db = await GetInMemoryDb();
        db.Konyvek.Add(new Konyv { Cim = "Könyv 1", Szerzo = "Szerzõ", Kiado = "Kiado", Kiadaseve = 2000 });
        db.Olvasok.Add(new Olvaso { Nev = "Olvasó 1", Cim = "Bp", SzuletesiDatum = new DateTime(1990, 1, 1) });
        await db.SaveChangesAsync();

        var konyvId = db.Konyvek.First().Id;
        var olvasoId = db.Olvasok.First().Id;

        var service = new KolcsonzesService(db);
        await service.KolcsonzesAsync(new Kolcsonzes
        {
            KonyvId = konyvId,
            OlvasoId = olvasoId,
            KolcsonzesDatuma = DateTime.Now.AddDays(1),
            VisszahozasHatarideje = DateTime.Now.AddDays(10)
        });

        var result = await db.Kolcsonzesek.FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(konyvId, result.KonyvId);
        Assert.Equal(olvasoId, result.OlvasoId);
    }

    [Fact]
    public async Task Lista_ReturnsKolcsonzesek()
    {
        var db = await GetInMemoryDb();
        db.Kolcsonzesek.Add(new Kolcsonzes
        {
            KonyvId = 1,
            OlvasoId = 1,
            KolcsonzesDatuma = DateTime.Now.AddDays(1),
            VisszahozasHatarideje = DateTime.Now.AddDays(5)
        });
        await db.SaveChangesAsync();

        var service = new KolcsonzesService(db);
        var result = await service.Lista();

        Assert.Single(result);
    }
}
