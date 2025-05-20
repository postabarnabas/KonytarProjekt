//using Posta_Barnabas_Projekt.Modellek;

namespace Posta_Barnabas_Projekt.Services
{
    public interface IKönyvService
    {
        Task<List<Könyv>> ListázásAsync();
        Task<Könyv> LekérésAsync(int id);
        Task<Könyv> LétrehozásAsync(Könyv könyv);
        Task<Könyv> MódosításAsync(int id, Könyv könyv);
        Task<bool> TörlésAsync(int d);
    }
}
