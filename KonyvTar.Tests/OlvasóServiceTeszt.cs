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
        var service = new OlvasoService(db);
        var olvaso = new Olvaso { Nev = "Teszt Elek", Cim = "Debrecen", SzuletesiDatum = new DateTime(1990, 1, 1) };

        await service.HozzaadasAsync(olvaso);
        var result = await db.Olvasok.FirstOrDefaultAsync();

        Assert.NotNull(result);
        Assert.Equal("Teszt Elek", result.Nev);
    }

    [Fact]
    public async Task ListazAsync_ReturnsOlvasok()
    {
        var db = await GetInMemoryDb();
        db.Olvasok.Add(new Olvaso { Nev = "Olvasó 1", Cim = "Bp", SzuletesiDatum = new DateTime(1980, 1, 1) });
        await db.SaveChangesAsync();

        var service = new OlvasoService(db);
        var result = await service.ListazAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task TorlesAsync_DeletesOlvaso()
    {
        var db = await GetInMemoryDb();
        var olvaso = new Olvaso { Nev = "Olvasó 1", Cim = "Bp", SzuletesiDatum = new DateTime(1980, 1, 1) };
        db.Olvasok.Add(olvaso);
        await db.SaveChangesAsync();

        var service = new OlvasoService(db);
        await service.TorlesAsync(olvaso.Id);
        var result = await db.Olvasok.FindAsync(olvaso.Id);

        Assert.Null(result);
    }
}
