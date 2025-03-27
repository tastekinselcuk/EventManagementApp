using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Services
{
    public interface IParticipantService
    {
        Task<IEnumerable<ParticipantDTO>> GetAllParticipantsAsync();
        Task<ParticipantDTO> GetParticipantByIdAsync(int id);
        Task<ParticipantDTO> CreateParticipantAsync(ParticipantDTO participantDto);
        Task<bool> UpdateParticipantAsync(ParticipantDTO participantDto);
        Task<bool> DeleteParticipantAsync(int id);
        Task<IEnumerable<ParticipantDTO>> GetParticipantsByEventIdAsync(int eventId);
        Task<IEnumerable<ParticipantDTO>> GetAvailableParticipantsForEventAsync(int eventId);
    }
}
