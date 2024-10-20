using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Email is Invalid")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
