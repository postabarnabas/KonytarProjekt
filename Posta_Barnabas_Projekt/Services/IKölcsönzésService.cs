//using Posta_Barnabas_Projekt.Modellek;

public interface IKölcsönzésService
{
    Task<List<Kölcsönzés>> ListázásAsync();
    Task<Kölcsönzés> LétrehozásAsync(Kölcsönzés kolcsonzes);
    Task<List<Kölcsönzés>> LekérésOlvasóSzerintAsync(int olvasóId);
}
