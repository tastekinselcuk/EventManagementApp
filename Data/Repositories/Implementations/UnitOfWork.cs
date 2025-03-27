using EventManagementApp.Data.Repositories;
using EventManagementApp.Models;

namespace EventManagementApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IEventRepository Events { get; }
        public IParticipantRepository Participants { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Events = new EventRepository(context);
            Participants = new ParticipantRepository(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
