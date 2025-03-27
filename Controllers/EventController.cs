using Microsoft.AspNetCore.Mvc;
using EventManagementApp.Services;
using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;

        public EventController(IEventService eventService, IParticipantService participantService)
        {
            _eventService = eventService;
            _participantService = participantService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            if (eventDto == null) return NotFound();
            return View(eventDto);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Participants = await _participantService.GetAllParticipantsAsync();
            return View(new EventDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventDTO eventDto)
        {
            if (ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(eventDto);
                return RedirectToAction(nameof(Index), "Home");
            }
            ViewBag.Participants = await _participantService.GetAllParticipantsAsync();
            return View(eventDto);
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetUpcomingEventsAsync();
            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(int eventId, int participantId)
        {
            var result = await _eventService.AddParticipantToEventAsync(eventId, participantId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveParticipant(int eventId, int participantId)
        {
            var result = await _eventService.RemoveParticipantFromEventAsync(eventId, participantId);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipants(int id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            if (eventDto?.Participants == null) return Json(new List<ParticipantDTO>());
            return Json(eventDto.Participants);
        }
    }
}
