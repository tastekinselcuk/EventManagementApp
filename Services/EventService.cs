using AutoMapper;
using EventManagementApp.Data.Repositories;
using EventManagementApp.Models;
using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDTO>> GetUpcomingEventsAsync()
        {
            var events = await _eventRepository.GetUpcomingEventsAsync();
            var eventDtos = _mapper.Map<IEnumerable<EventDTO>>(events);
            return eventDtos;
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
        {
            var @event = await _eventRepository.GetEventWithParticipantsAsync(id);
            if (@event == null) return null;

            var eventDto = _mapper.Map<EventDTO>(@event);
            eventDto.Participants = _mapper.Map<List<ParticipantDTO>>(@event.EventParticipants.Select(ep => ep.Participant));
            return eventDto;
        }

        public async Task<EventDTO> CreateEventAsync(EventDTO eventDto)
        {
            var @event = _mapper.Map<Event>(eventDto);
            await _eventRepository.AddAsync(@event);
            await _eventRepository.SaveChangesAsync();

            // Add participants if any selected
            if (eventDto.ParticipantIds != null && eventDto.ParticipantIds.Any())
            {
                foreach (var participantId in eventDto.ParticipantIds)
                {
                    await _eventRepository.AddParticipantToEventAsync(@event.Id, participantId);
                }
            }

            return _mapper.Map<EventDTO>(@event);
        }

        public async Task<bool> UpdateEventAsync(EventDTO eventDto)
        {
            var @event = await _eventRepository.GetByIdAsync(eventDto.Id);
            if (@event == null) return false;
            
            _mapper.Map(eventDto, @event);
            _eventRepository.Update(@event);
            return await _eventRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null) return false;

            _eventRepository.Delete(@event);
            return await _eventRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddParticipantToEventAsync(int eventId, int participantId)
        {
            var result = await _eventRepository.AddParticipantToEventAsync(eventId, participantId);
            if (result)
            {
                // Update participant's IsAttending status
                await _eventRepository.UpdateParticipantAttendanceStatus(participantId, true);
            }
            return result;
        }

        public async Task<bool> RemoveParticipantFromEventAsync(int eventId, int participantId)
        {
            var result = await _eventRepository.RemoveParticipantFromEventAsync(eventId, participantId);
            if (result)
            {
                // Update participant's IsAttending status
                await _eventRepository.UpdateParticipantAttendanceStatus(participantId, false);
            }
            return result;
        }
    }
}
