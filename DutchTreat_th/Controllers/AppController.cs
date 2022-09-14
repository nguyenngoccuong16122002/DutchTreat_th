using DutchTreat_th.Data;
using DutchTreat_th.Services;
using DutchTreat_th.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;
        private readonly DutchDbContext _dutchDbContext;

        public AppController(IMailService mailService, IDutchRepository repository,DutchDbContext dutchDbContext)
        {
            _mailService = mailService;
            _repository = repository;
            _dutchDbContext = dutchDbContext;
        }

        public IActionResult Index()
        {
            var results = _dutchDbContext.products.ToList();
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send the Email
                _mailService.SendMessage("shawn@wildermuth.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent...";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllsProducts();

            return View(results);
        }
    }
}
