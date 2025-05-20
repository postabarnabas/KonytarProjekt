using System.ComponentModel.DataAnnotations;

public class Kölcsönzés
{
    public int Id { get; set; }

    [Required]
    public int OlvasóId { get; set; }

    [Required]
    public int KönyvId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Kölcsönzés), nameof(EllenőrizKölcsönzésDátum))]
    public DateTime KölcsönzésIdeje { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Kölcsönzés), nameof(EllenőrizVisszahozásDátum))]
    public DateTime VisszahozásiHatáridő { get; set; }

    public static ValidationResult? EllenőrizKölcsönzésDátum(DateTime dátum, ValidationContext context)
    {
        if (dátum.Date < DateTime.Today)
        {
            return new ValidationResult("A kölcsönzés ideje nem lehet a mai napnál korábbi.");
        }
        return ValidationResult.Success;
    }

    public static ValidationResult? EllenőrizVisszahozásDátum(DateTime dátum, ValidationContext context)
    {
        var instance = context.ObjectInstance as Kölcsönzés;
        if (instance == null) return ValidationResult.Success;

        if (dátum <= instance.KölcsönzésIdeje)
        {
            return new ValidationResult("A visszahozási határidő későbbi kell legyen, mint a kölcsönzés ideje.");
        }

        return ValidationResult.Success;
    }
}
