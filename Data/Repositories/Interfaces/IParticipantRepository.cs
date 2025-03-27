using EventManagementApp.Models;

namespace EventManagementApp.Data.Repositories
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId);
        Task<IEnumerable<Participant>> GetAvailableParticipantsForEventAsync(int eventId);
        Task<bool> UpdateParticipationStatusAsync(int participantId, bool isAttending);
    }
}
