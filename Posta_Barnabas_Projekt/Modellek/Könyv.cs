using System.ComponentModel.DataAnnotations;
namespace Posta_Barnabas_Projekt.Modellek
{
    public class Könyv
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        [StringLength(100, ErrorMessage = "A cím maximum 100 karakter lehet.")]
        public string Cím { get; set; } = string.Empty;

        [Required(ErrorMessage = "A szerző megadása kötelező.")]
        [StringLength(100)]
        public string Szerző { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kiadó megadása kötelező.")]
        [StringLength(100)]
        public string Kiadó { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "A kiadás éve nem lehet negatív.")]
        public int KiadásÉve { get; set; }
    }
}
