using Moq;
using Xunit;
using Posta_Barnabas_Projekt.Modellek;
using Posta_Barnabas_Projekt.Data;
using Posta_Barnabas_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

public class OlvasoServiceTests
{
    private async Task<KönyvtárAdatbázis> GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<KönyvtárAdatbázis>()
            .UseInMemoryDatabase(databaseName: "OlvasoTestDb")
            .Options;
        var db = new KönyvtárAdatbázis(options);
        await db.Database.EnsureCreatedAsync();
        return db;
    }

    [Fact]
    public async Task HozzaadasAsync_AddsOlvaso()
    {
        var db = await GetInMemoryDb();
        var service = new OlvasóService(db);
        var olvaso = new Olvasó { Név = "Teszt Elek", Lakcím = "Debrecen", SzületésiDátum = new DateTime(1990, 1, 1) };

        await service.LétrehozásAsync(olvaso);
        var result = await db.Olvasók.FindAsync(olvaso.Id);

        Assert.NotNull(result);
        Assert.Equal("Teszt Elek", result.Név);
    }

    [Fact]
    public async Task ListazAsync_ReturnsOlvasok()
    {
        var db = await GetInMemoryDb();
        db.Olvasók.Add(new Olvasó { Név = "Teszt ELek", Lakcím = "Debrecen", SzületésiDátum = new DateTime(1990, 1, 1) });
        await db.SaveChangesAsync();

        var service = new OlvasóService(db);
        var result = await service.ListázásAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task TorlesAsync_DeletesOlvaso()
    {
        var db = await GetInMemoryDb();
        var olvaso = new Olvasó { Név = "Olvasó 1", Lakcím = "Bp", SzületésiDátum = new DateTime(1980, 1, 1) };
        db.Olvasók.Add(olvaso);
        await db.SaveChangesAsync();

        var service = new OlvasóService(db);
        await service.TörlésAsync(olvaso.Id);
        var result = await db.Olvasók.FindAsync(olvaso.Id);

        Assert.Null(result);
    }
}
