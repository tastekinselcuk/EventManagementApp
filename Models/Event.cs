using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Details { get; set; } = string.Empty;

        public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}