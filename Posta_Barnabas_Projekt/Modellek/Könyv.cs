using System.ComponentModel.DataAnnotations;
namespace Posta_Barnabas_Projekt.Modellek
{
    public class Könyv
    {
        [Key]
        public int LeltáriSzám { get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage ="A címnek tilos üresnek vagy csak egy szóköznek lennie.")]
        public string Cím { get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "A szerző nevének tilos üresnek vagy csak egy szóköznek lennie.")]
        public string Szerző {  get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "A kiadó nevének tilos üresnek vagy csak egy szóköznek lennie.")]
        public string Kiadó {  get; set; }

        [Range(0,int.MaxValue, ErrorMessage ="A kiadás éve nem lehet negatív")]
        public int KiadásÉve {  get; set; }

    }
}
