using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld4.ViewModels;
using TheWorld4.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld4.Controllers
{
    public class AppController : Controller
    {
        public IMailService _mailService { get; set; }
        public AppController(IMailService service)
        {
            _mailService = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email");
                }
                else
                {
                    if(_mailService.SendMail(model.Email, email, $"Contact from {model.Name} ({model.Email})", model.Message))
                    {
                        ModelState.Clear();
                        ViewBag.SuccessMessage = "Email has been sent. Thanks!";
                    }
                }

                
            }
            return View();
        }
    }
}
