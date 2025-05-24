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
    private async Task<K�nyvt�rAdatb�zis> GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<K�nyvt�rAdatb�zis>()
            .UseInMemoryDatabase(databaseName: "KolcsonzesTestDb")
            .Options;
        var db = new K�nyvt�rAdatb�zis(options);
        await db.Database.EnsureCreatedAsync();
        return db;
    }

    [Fact]
    public async Task KolcsonzesAsync_AddsKolcsonzes()
    {
        var db = await GetInMemoryDb();
        db.K�nyvek.Add(new K�nyv { C�m = "K�nyv 1", Szerz� = "Szerz�", Kiad� = "Kiado", Kiad�s�ve = 2000 });
        db.Olvas�k.Add(new Olvas� { N�v = "Olvas� 1", Lakc�m = "Bp", Sz�let�siD�tum = new DateTime(1990, 1, 1) });
        await db.SaveChangesAsync();

        var konyvId = db.K�nyvek.First().Id;
        var olvasoId = db.Olvas�k.First().Id;

        var service = new K�lcs�nz�sService(db);
        await service.L�trehoz�sAsync(new K�lcs�nz�s
        {
            K�nyvId = konyvId,
            Olvas�Id = olvasoId,
            K�lcs�nz�sIdeje = DateTime.Now.AddDays(1),
            Visszahoz�siHat�rid� = DateTime.Now.AddDays(10)
        });

        var result = await db.K�lcs�nz�sek.FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(konyvId, result.K�nyvId);
        Assert.Equal(olvasoId, result.Olvas�Id);
    }

    [Fact]
    public async Task Lista_ReturnsKolcsonzesek()
    {
        var db = await GetInMemoryDb();
        db.K�lcs�nz�sek.Add(new K�lcs�nz�s
        {
            K�nyvId = 1,
            Olvas�Id = 1,
            K�lcs�nz�sIdeje = DateTime.Now.AddDays(1),
            Visszahoz�siHat�rid� = DateTime.Now.AddDays(5)
        });
        await db.SaveChangesAsync();

        var service = new K�lcs�nz�sService(db);
        var result = await service.List�z�sAsync();


        Assert.Single(result);
    }
}
