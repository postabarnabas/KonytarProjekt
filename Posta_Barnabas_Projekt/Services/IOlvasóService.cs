using Posta_Barnabas_Projekt.Modellek;

public interface IOlvasóService
{
    Task<List<Olvasó>> ListázásAsync();
    Task<Olvasó> LekérésAsync(int id);
    Task<Olvasó> LétrehozásAsync(Olvasó olvasó);
    Task<Olvasó> MódosításAsync(int id, Olvasó olvasó);
    Task<bool> TörlésAsync(int id);
}
