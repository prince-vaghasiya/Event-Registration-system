using System.ComponentModel.DataAnnotations;

public class FormField
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EventID { get; set; }

    [Required]
    public string FieldType { get; set; }

    [Required]
    public string FieldLabel { get; set; }

    [Required]
    public bool IsRequired { get; set; }
    public Event Event { get; set; }
}
