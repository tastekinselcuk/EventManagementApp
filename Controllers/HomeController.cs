using Microsoft.AspNetCore.Mvc;
using EventManagementApp.Services;
using EventManagementApp.Models.DTOs;

namespace EventManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var upcomingEvents = await _eventService.GetUpcomingEventsAsync();
            return View(upcomingEvents);
        }
    }
}
