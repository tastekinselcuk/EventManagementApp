using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementApp.Models;
public class EventParticipant
{
    [Key, Column(Order = 1)]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Key, Column(Order = 2)]
    public int ParticipantId { get; set; }
    public Participant Participant { get; set; } = null!;
}

public enum ParticipationStatus
{
    Pending,
    Confirmed,
    Cancelled
}
