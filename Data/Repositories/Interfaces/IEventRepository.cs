using EventManagementApp.Models;

namespace EventManagementApp.Data.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<Event?> GetEventWithParticipantsAsync(int eventId);
        Task<bool> AddParticipantToEventAsync(int eventId, int participantId);
        Task<bool> RemoveParticipantFromEventAsync(int eventId, int participantId);
        Task<bool> UpdateParticipantAttendanceStatus(int participantId, bool isAttending);
    }
}