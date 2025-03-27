using Microsoft.AspNetCore.Mvc;
using EventManagementApp.Services;
using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        public IActionResult Create()
        {
            return View(new ParticipantDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParticipantDTO participantDto)
        {
            if (ModelState.IsValid)
            {
                await _participantService.CreateParticipantAsync(participantDto);
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(participantDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableParticipants(int eventId)
        {
            var participants = await _participantService.GetAvailableParticipantsForEventAsync(eventId);
            return Json(participants);
        }
    }
}
