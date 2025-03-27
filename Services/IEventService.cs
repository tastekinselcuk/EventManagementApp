using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetUpcomingEventsAsync();
        Task<EventDTO> GetEventByIdAsync(int id);
        Task<EventDTO> CreateEventAsync(EventDTO eventDto);
        Task<bool> UpdateEventAsync(EventDTO eventDto);
        Task<bool> DeleteEventAsync(int id);
        Task<bool> AddParticipantToEventAsync(int eventId, int participantId);
        Task<bool> RemoveParticipantFromEventAsync(int eventId, int participantId);
    }
}
