using Microsoft.EntityFrameworkCore;
using Posta_Barnabas_Projekt.Data;
using Posta_Barnabas_Projekt.Modellek;

public class OlvasóService : IOlvasóService
{
    private readonly KönyvtárAdatbázis _context;

    public OlvasóService(KönyvtárAdatbázis context)
    {
        _context = context;
    }

    public async Task<List<Olvasó>> ListázásAsync()
    {
        return await _context.Olvasók.ToListAsync();
    }

    public async Task<Olvasó> LekérésAsync(int id)
    {
        return await _context.Olvasók.FindAsync(id);
    }

    public async Task<Olvasó> LétrehozásAsync(Olvasó olvasó)
    {
        _context.Olvasók.Add(olvasó);
        await _context.SaveChangesAsync();
        return olvasó;
    }

    public async Task<Olvasó> MódosításAsync(int id, Olvasó olvasó)
    {
        var meglévő = await _context.Olvasók.FindAsync(id);
        if (meglévő == null) return null;

        meglévő.Név = olvasó.Név;
        meglévő.Lakcím = olvasó.Lakcím;
        meglévő.SzületésiDátum = olvasó.SzületésiDátum;

        await _context.SaveChangesAsync();
        return meglévő;
    }

    public async Task<bool> TörlésAsync(int id)
    {
        var olvasó = await _context.Olvasók.FindAsync(id);
        if (olvasó == null) return false;

        _context.Olvasók.Remove(olvasó);
        await _context.SaveChangesAsync();
        return true;
    }
}
