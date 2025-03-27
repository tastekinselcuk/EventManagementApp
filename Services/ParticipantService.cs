using AutoMapper;
using EventManagementApp.Data.Repositories;
using EventManagementApp.Models;
using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantService(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParticipantDTO>> GetAllParticipantsAsync()
        {
            var participants = await _participantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParticipantDTO>>(participants);
        }

        public async Task<ParticipantDTO> GetParticipantByIdAsync(int id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            return _mapper.Map<ParticipantDTO>(participant);
        }

        public async Task<ParticipantDTO> CreateParticipantAsync(ParticipantDTO participantDto)
        {
            var participant = _mapper.Map<Participant>(participantDto);
            await _participantRepository.AddAsync(participant);
            await _participantRepository.SaveChangesAsync();
            return _mapper.Map<ParticipantDTO>(participant);
        }

        public async Task<bool> UpdateParticipantAsync(ParticipantDTO participantDto)
        {
            var participant = await _participantRepository.GetByIdAsync(participantDto.Id);
            if (participant == null) return false;

            _mapper.Map(participantDto, participant);
            _participantRepository.Update(participant);
            return await _participantRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteParticipantAsync(int id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            if (participant == null) return false;

            _participantRepository.Delete(participant);
            return await _participantRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ParticipantDTO>> GetParticipantsByEventIdAsync(int eventId)
        {
            var participants = await _participantRepository.GetParticipantsByEventIdAsync(eventId);
            return _mapper.Map<IEnumerable<ParticipantDTO>>(participants);
        }

        public async Task<IEnumerable<ParticipantDTO>> GetAvailableParticipantsForEventAsync(int eventId)
        {
            var participants = await _participantRepository.GetAvailableParticipantsForEventAsync(eventId);
            return _mapper.Map<IEnumerable<ParticipantDTO>>(participants);
        }
    }
}
