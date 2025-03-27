using AutoMapper;
using EventManagementApp.Models;
using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.ParticipantIds, 
                    opt => opt.MapFrom(src => src.EventParticipants.Select(ep => ep.ParticipantId)));
            CreateMap<EventDTO, Event>();

            CreateMap<Participant, ParticipantDTO>();
            CreateMap<ParticipantDTO, Participant>();
        }
    }
}
