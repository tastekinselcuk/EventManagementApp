using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Models.DTOs
{
    public class EventDTO
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

        [Display(Name = "Participants")]
        public List<int> ParticipantIds { get; set; } = new();
        public List<ParticipantDTO>? Participants { get; set; }
    }
}
