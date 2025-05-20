using System.ComponentModel.DataAnnotations;

public class Olvasó
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A név megadása kötelező.")]
    [StringLength(100)]
    public string Név { get; set; } = string.Empty;

    [Required(ErrorMessage = "A lakcím megadása kötelező.")]
    [StringLength(200)]
    public string Lakcím { get; set; } = string.Empty;

    [Required(ErrorMessage = "A születési dátum megadása kötelező.")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Olvasó), nameof(EllenőrizSzületésiDátum))]
    public DateTime SzületésiDátum { get; set; }

    public static ValidationResult? EllenőrizSzületésiDátum(DateTime dátum, ValidationContext context)
    {
        if (dátum.Year < 1900)
        {
            return new ValidationResult("A születési év nem lehet 1900 előtt.");
        }
        return ValidationResult.Success;
    }
}
