using EventManagementApp.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventParticipant>()
            .HasKey(ep => new { ep.EventId, ep.ParticipantId });

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(ep => ep.EventId);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Participant)
            .WithMany(p => p.EventParticipants)
            .HasForeignKey(ep => ep.ParticipantId);
    }
}
