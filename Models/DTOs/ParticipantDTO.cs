using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Models.DTOs
{
    public class ParticipantDTO
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
    }
}
