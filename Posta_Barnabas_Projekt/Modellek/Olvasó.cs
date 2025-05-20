using System.ComponentModel.DataAnnotations;
namespace Posta_Barnabas_Projekt.Modellek
{
    public class Olvasó
    {
        [Key]
        public int OlvasóSzám {  get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "A névnek tilos üresnek vagy csak egy szóköznek lennie.")]
        public string Név {  get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "A lakcímnek tilos üresnek vagy csak egy szóköznek lennie.")]
        public string Lakcím { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime),"1900-01-01","2025. 05. 15.",ErrorMessage ="A születési dátum nem lehet 1900 előtt.")]
        public DateTime SzületésiDátum { get; set; }
    }
}
