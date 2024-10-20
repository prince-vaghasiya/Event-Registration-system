using EventRegistrationSystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrganizerUserID { get; set; }

    [Required]
    public string EventName { get; set; }

    public string? EventDescription { get; set; }

    [DataType(DataType.Date)]
    [Required]
    public DateTime DeadLine { get; set; }

    [DefaultValue(int.MaxValue)]
    public int MaxParticipants { get; set; }

    public string? EventBannerURL { get; set; }

    public User Organizer { get; set; }
    public ICollection<FormField> FormFields { get; set; }
}
