using Microsoft.EntityFrameworkCore;
using Posta_Barnabas_Projekt.Data;
using Posta_Barnabas_Projekt.Modellek;

public class KölcsönzésService : IKölcsönzésService
{
    private readonly KönyvtárAdatbázis _context;

    public KölcsönzésService(KönyvtárAdatbázis context)
    {
        _context = context;
    }

    public async Task<List<Kölcsönzés>> ListázásAsync()
    {
        return await _context.Kölcsönzések
            .Include(k => k.Könyv==Könyv)
            .Include(k => k.Olvasó)
            .ToListAsync();
    }

    public async Task<Kölcsönzés> LétrehozásAsync(Kölcsönzés kolcsonzes)
    {
        _context.Kölcsönzések.Add(kolcsonzes);
        await _context.SaveChangesAsync();
        return kolcsonzes;
    }

    public async Task<List<Kölcsönzés>> LekérésOlvasóSzerintAsync(int olvasóId)
    {
        return await _context.Kölcsönzések
            .Where(k => k.OlvasóId == olvasóId)
            .Include(k => k.Könyv)
            .ToListAsync();
    }
}
