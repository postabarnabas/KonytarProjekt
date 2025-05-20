using Microsoft.EntityFrameworkCore;
using Posta_Barnabas_Projekt.Data;
//using Posta_Barnabas_Projekt.Modellek;
using Posta_Barnabas_Projekt.Services;

public class KönyvService : IKönyvService
{
    private readonly KönyvtárAdatbázis _context;

    public KönyvService(KönyvtárAdatbázis context)
    {
        _context = context;
    }

    public async Task<List<Könyv>> ListázásAsync()
    {
        return await _context.Könyvek.ToListAsync();
    }

    public async Task<Könyv> LekérésAsync(int id)
    {
        return await _context.Könyvek.FindAsync(id);
    }

    public async Task<Könyv> LétrehozásAsync(Könyv könyv)
    {
        _context.Könyvek.Add(könyv);
        await _context.SaveChangesAsync();
        return könyv;
    }

    public async Task<Könyv> MódosításAsync(int id, Könyv könyv)
    {
        var meglévő = await _context.Könyvek.FindAsync(id);
        if (meglévő == null) return null;

        meglévő.Cím = könyv.Cím;
        meglévő.Szerző = könyv.Szerző;
        meglévő.Kiadó = könyv.Kiadó;
        meglévő.KiadásÉve = könyv.KiadásÉve;

        await _context.SaveChangesAsync();
        return meglévő;
    }

    public async Task<bool> TörlésAsync(int id)
    {
        var könyv = await _context.Könyvek.FindAsync(id);
        if (könyv == null) return false;

        _context.Könyvek.Remove(könyv);
        await _context.SaveChangesAsync();
        return true;
    }
}
