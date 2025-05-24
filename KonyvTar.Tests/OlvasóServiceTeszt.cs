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
    private async Task<K�nyvt�rAdatb�zis> GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<K�nyvt�rAdatb�zis>()
            .UseInMemoryDatabase(databaseName: "OlvasoTestDb")
            .Options;
        var db = new K�nyvt�rAdatb�zis(options);
        await db.Database.EnsureCreatedAsync();
        return db;
    }

    [Fact]
    public async Task HozzaadasAsync_AddsOlvaso()
    {
        var db = await GetInMemoryDb();
        var service = new Olvas�Service(db);
        var olvaso = new Olvas� { N�v = "Teszt Elek", Lakc�m = "Debrecen", Sz�let�siD�tum = new DateTime(1990, 1, 1) };

        await service.L�trehoz�sAsync(olvaso);
        var result = await db.Olvas�k.FindAsync(olvaso.Id);

        Assert.NotNull(result);
        Assert.Equal("Teszt Elek", result.N�v);
    }

    [Fact]
    public async Task ListazAsync_ReturnsOlvasok()
    {
        var db = await GetInMemoryDb();
        db.Olvas�k.Add(new Olvas� { N�v = "Teszt ELek", Lakc�m = "Debrecen", Sz�let�siD�tum = new DateTime(1990, 1, 1) });
        await db.SaveChangesAsync();

        var service = new Olvas�Service(db);
        var result = await service.List�z�sAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task TorlesAsync_DeletesOlvaso()
    {
        var db = await GetInMemoryDb();
        var olvaso = new Olvas� { N�v = "Olvas� 1", Lakc�m = "Bp", Sz�let�siD�tum = new DateTime(1980, 1, 1) };
        db.Olvas�k.Add(olvaso);
        await db.SaveChangesAsync();

        var service = new Olvas�Service(db);
        await service.T�rl�sAsync(olvaso.Id);
        var result = await db.Olvas�k.FindAsync(olvaso.Id);

        Assert.Null(result);
    }
}
