using EventManagementApp.Data.Repositories;

namespace EventManagementApp.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        IParticipantRepository Participants { get; }
        Task<int> SaveChangesAsync();
    }
}
