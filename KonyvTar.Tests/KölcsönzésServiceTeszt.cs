using Moq;
using Xunit;
using Posta_Barnabas_Projekt.Modellek;
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
        db.Könyvek.Add(new Könyv { Cím = "Könyv 1", Szerzõ = "Szerzõ", Kiadó = "Kiado", KiadásÉve = 2000 });
        db.Olvasók.Add(new Olvasó { Név = "Olvasó 1", Lakcím = "Bp", SzületésiDátum = new DateTime(1990, 1, 1) });
        await db.SaveChangesAsync();

        var konyvId = db.Könyvek.First().Id;
        var olvasoId = db.Olvasók.First().Id;

        var service = new KölcsönzésService(db);
        await service.LétrehozásAsync(new Kölcsönzés
        {
            KönyvId = konyvId,
            OlvasóId = olvasoId,
            KölcsönzésIdeje = DateTime.Now.AddDays(1),
            VisszahozásiHatáridõ = DateTime.Now.AddDays(10)
        });

        var result = await db.Kölcsönzések.FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(konyvId, result.KönyvId);
        Assert.Equal(olvasoId, result.OlvasóId);
    }

    [Fact]
    public async Task Lista_ReturnsKolcsonzesek()
    {
        var db = await GetInMemoryDb();

        var konyv = new Könyv { Cím = "Cím", Szerzõ = "Szerzõ", Kiadó = "Kiadó", KiadásÉve = 2000 };
        var olvaso = new Olvasó { Név = "Olvasó 1", Lakcím = "Bp", SzületésiDátum = new DateTime(1990, 1, 1) };

        db.Könyvek.Add(konyv);
        db.Olvasók.Add(olvaso);
        await db.SaveChangesAsync();

        db.Kölcsönzések.Add(new Kölcsönzés
        {
            KönyvId = konyv.Id,
            OlvasóId = olvaso.Id,
            KölcsönzésIdeje = DateTime.Now.AddDays(1),
            VisszahozásiHatáridõ = DateTime.Now.AddDays(5)
        });
        await db.SaveChangesAsync();

        var service = new KölcsönzésService(db);
        var result = await service.ListázásAsync();

        Assert.Single(result);
    }
}
