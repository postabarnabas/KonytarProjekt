using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
namespace Posta_Barnabas_Projekt.Modellek
{
    public class Kölcsönzés
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Olvasó")]
        public int OlvasóSzám {  get; set; }

        [Required]
        [ForeignKey("Könyv")]
        public int LeltáriSzám { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [CustomValidation(typeof(Kölcsönzés), nameof(ÉrvényesítésKölcsönzés))]
        public DateTime KölcsönzésDátuma {  get; set; }

        [DataType(DataType.Date)]
        [Required]
        [CustomValidation(typeof(Kölcsönzés), nameof(ÉrvényesítésVisszahozás))]
        public DateTime VisszahozásDátuma { get; set; }

        public static ValidationResult ÉrvényesítésKölcsönzés(DateTime date, ValidationContext context)
        {
            return date.Date < DateTime.Now.Date
                ? new ValidationResult("A kölcsönzés dátuma nem lehet a mai napnál korábbi.")
                : ValidationResult.Success;
        }
        public static ValidationResult ÉrvényesítésVisszahozás(DateTime returnDate,ValidationContext context)
        {
            var instance = context.ObjectInstance as Kölcsönzés;
            if(instance == null)
            {
                return ValidationResult.Success;
            }
            return returnDate > instance.KölcsönzésDátuma
                ? ValidationResult.Success
                : new ValidationResult("A visszahozás dátuma később kell legyen, mint a kölcsönzés ideje.");
        }

    }
}
