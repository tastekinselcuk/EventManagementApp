using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Models
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsAttending { get; set; }

        public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}