using EventRegistrationSystem.Models;
using System.ComponentModel.DataAnnotations;

public class Response
{
    public int Id { get; set; }

    [Required]
    public int UserID { get; set; }

    [Required]
    public int FieldID { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public string ResponseValue { get; set; }

    public FormField Field { get; set; }

}
