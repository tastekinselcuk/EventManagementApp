using EventManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApp.Data.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly AppDbContext _appDbContext;

        public EventRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            return await _dbSet
                .Include(e => e.EventParticipants)
                    .ThenInclude(ep => ep.Participant)
                .Where(e => e.Date >= DateTime.UtcNow)
                .OrderBy(e => e.Date)
                .ToListAsync();
        }

        public async Task<Event?> GetEventWithParticipantsAsync(int eventId)
        {
            return await _dbSet
                .Include(e => e.EventParticipants)
                    .ThenInclude(ep => ep.Participant)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<bool> AddParticipantToEventAsync(int eventId, int participantId)
        {
            try
            {
                var eventParticipant = new EventParticipant
                {
                    EventId = eventId,
                    ParticipantId = participantId
                };

                await _appDbContext.EventParticipants.AddAsync(eventParticipant);
                
                // Update participant attendance status
                var participant = await _appDbContext.Participants.FindAsync(participantId);
                if (participant != null)
                {
                    participant.IsAttending = true;
                    _appDbContext.Participants.Update(participant);
                }

                return await _appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveParticipantFromEventAsync(int eventId, int participantId)
        {
            var eventParticipant = await _appDbContext.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);

            if (eventParticipant == null) return false;

            _appDbContext.EventParticipants.Remove(eventParticipant);

            // Update participant attendance status
            var participant = await _appDbContext.Participants.FindAsync(participantId);
            if (participant != null)
            {
                participant.IsAttending = false;
                _appDbContext.Participants.Update(participant);
            }

            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateParticipantAttendanceStatus(int participantId, bool isAttending)
        {
            var participant = await _appDbContext.Participants.FindAsync(participantId);
            if (participant == null) return false;

            participant.IsAttending = isAttending;
            _appDbContext.Participants.Update(participant);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
