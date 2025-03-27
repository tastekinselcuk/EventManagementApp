using EventManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApp.Data.Repositories
{
    public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        private readonly AppDbContext _appDbContext;

        public ParticipantRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId)
        {
            return await _appDbContext.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Select(ep => ep.Participant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Participant>> GetAvailableParticipantsForEventAsync(int eventId)
        {
            var eventParticipantIds = await _appDbContext.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Select(ep => ep.ParticipantId)
                .ToListAsync();

            return await _dbSet
                .Where(p => !eventParticipantIds.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<bool> UpdateParticipationStatusAsync(int participantId, bool isAttending)
        {
            var participant = await _dbSet.FindAsync(participantId);
            if (participant == null) return false;

            participant.IsAttending = isAttending;
            _dbSet.Update(participant);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}