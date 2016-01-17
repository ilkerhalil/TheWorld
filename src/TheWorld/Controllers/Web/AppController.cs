
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IWorldRepository _worldRepository;

        public AppController(IMailService mailService, IWorldRepository worldRepository)
        {
            _mailService = mailService;
            _worldRepository = worldRepository;
        }

        public IActionResult Index()
        {
            //var trips = _worldRepository.GetAllTrips().OrderBy(or => or.Name).ToList();
            return View();
        }
        [Authorize]
        public IActionResult Trips()
        {
            //var trips = _worldRepository.GetAllTrips();
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid) return View();

            var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
            if (string.IsNullOrWhiteSpace(email)) ModelState.AddModelError("AppSettings:SiteEmailAddress", "Could not send email, configuration problem");
            if (!_mailService.SendMail(email, email, $"Contact Page from {contactViewModel.Name} ({contactViewModel.Email})", contactViewModel.Message)) return View();
            ModelState.Clear();
            ViewBag.Message = "Thank you,Sent E-Mail";
            return View();
        }

    }
}
